using Org.BouncyCastle.Crypto.Generators;
using PayrollCalculator.Application.DTOs.Auth;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Application.Interfaces.Services;
using PayrollCalculator.Domain.Entities;
using PayrollCalculator.Infrastructure.Persistence;
using PayrollCalculator.Application.Constants.Messages;
using PayrollCalculator.Application.Constants.StatusCodes;
using PayrollCalculator.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITenantRepository _tenantRepo;
        private readonly IBranchRepository _branchRepo;
        private readonly IUserRepository _userRepo;
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IAdminBranchMappingRepository _adminBranchRepo;
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;

        public AuthService(
            ITenantRepository tenantRepo,
            ITokenService tokenService,
            IBranchRepository branchRepo,
            IUserRepository userRepo,
            IUserRoleRepository userRoleRepo,
            IAdminBranchMappingRepository adminBranchRepo,
            AppDbContext context)
        {
            _tenantRepo = tenantRepo;
            _tokenService = tokenService;
            _branchRepo = branchRepo;
            _userRepo = userRepo;
            _userRoleRepo = userRoleRepo;
            _adminBranchRepo = adminBranchRepo;
            _context = context;
        }

        public async Task<ApiResponse<object>> SuperAdminSignupAsync(SuperAdminSignupRequest request)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                if (await _userRepo.EmailExistsAsync(request.Email))
                {
                    return new ApiResponse<object>(
                        StatusCodes.Conflict,
                        Signup.EmailAlreadyExists
                    );
                }

                var tenant = await _tenantRepo.AddAsync(new Tenant
                {
                    TenantName = request.TenantName,
                    SubDomain = request.SubDomain,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });

                var branch = await _branchRepo.AddAsync(new Branch
                {
                    TenantId = tenant.TenantId,
                    BranchName = request.BranchName,
                    IsHeadOffice = true,
                    Isactive = true
                });

                var user = await _userRepo.AddAsync(new User
                {
                    TenantId = tenant.TenantId,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });

                await _userRoleRepo.AddAsync(new UserRole
                {
                    UserId = user.UserId,
                    RoleId = 1 // SuperAdmin
                });

                await _adminBranchRepo.AddAsync(new AdminBranchMapping
                {
                    UserId = user.UserId,
                    BranchId = branch.BranchId,
                    IsPrimary = true
                });

                await tx.CommitAsync();

                return new ApiResponse<object>(
                    StatusCodes.Created,
                    Signup.SuperAdminCreatedSuccessfully,
                    new
                    {
                        TenantId = tenant.TenantId,
                        AdminUserId = user.UserId
                    }
                );

            }
            catch
            {
                await tx.RollbackAsync();

                return new ApiResponse<object>(
                    StatusCodes.InternalServerError,
                    Signup.SignupFailed
                );
            }
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new ApiResponse<LoginResponse>(
                    StatusCodes.Unauthorized,
                    Login.InvalidCredentials
                );
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _context.RefreshTokens.AddAsync(new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            });

            await _context.SaveChangesAsync();

            return new ApiResponse<LoginResponse>(
                StatusCodes.Success,
                Login.LoginSuccess,
                new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    UserId = user.UserId,
                    TenantId = user.TenantId
                }
            );
        }

    }

}
