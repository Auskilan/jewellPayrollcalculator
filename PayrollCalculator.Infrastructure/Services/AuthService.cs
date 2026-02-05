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
        private readonly IPasswordResetOtpRepository _otpRepo;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;

        public AuthService(
            ITenantRepository tenantRepo,
            ITokenService tokenService,
            IBranchRepository branchRepo,
            IUserRepository userRepo,
            IUserRoleRepository userRoleRepo,
            IAdminBranchMappingRepository adminBranchRepo,
            IPasswordResetOtpRepository otpRepo,
            IEmailService emailService,
            AppDbContext context)
        {
            _tenantRepo = tenantRepo;
            _tokenService = tokenService;
            _branchRepo = branchRepo;
            _userRepo = userRepo;
            _userRoleRepo = userRoleRepo;
            _adminBranchRepo = adminBranchRepo;
            _otpRepo = otpRepo;
            _emailService = emailService;
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

                var tenant = await _tenantRepo.CreateAsync(new Tenant
                {
                    OrganizationId = null, // Will be set when shop details are provided
                    TenantName = request.TenantName,
                    SubDomain = request.SubDomain,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });

                var branch = await _branchRepo.CreateAsync(new Branch
                {
                    TenantId = tenant.TenantId,
                    OrganizationId = null, // Will be set when shop details are provided
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

        public async Task<ApiResponse<object>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            try
            {
                // Check if user exists
                if (!await _userRepo.EmailExistsAsync(request.Email))
                {
                    return new ApiResponse<object>(
                        StatusCodes.NotFound,
                        ResetPassword.EmailNotFound
                    );
                }

                // Invalidate any existing OTPs for this email
                await _otpRepo.InvalidateOtpsForEmailAsync(request.Email);

                // Generate 6-digit OTP
                var otpCode = new Random().Next(100000, 999999).ToString();

                // Create OTP record
                var otp = new PasswordResetOtp
                {
                    Email = request.Email,
                    OtpCode = otpCode,
                    ExpiryTime = DateTime.UtcNow.AddMinutes(10), // 10 minutes expiry
                    IsUsed = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _otpRepo.AddAsync(otp);

                // Send OTP via email
                var emailSent = await _emailService.SendOtpEmailAsync(request.Email, otpCode);

                if (!emailSent)
                {
                    return new ApiResponse<object>(
                        StatusCodes.InternalServerError,
                        ResetPassword.FailedToSendOtp
                    );
                }

                return new ApiResponse<object>(
                    StatusCodes.Success,
                    ResetPassword.OtpSentSuccessfully
                );
            }
            catch
            {
                return new ApiResponse<object>(
                    StatusCodes.InternalServerError,
                    ResetPassword.FailedToSendOtp
                );
            }
        }

        public async Task<ApiResponse<object>> VerifyOtpAsync(VerifyOtpRequest request)
        {
            try
            {
                var otp = await _otpRepo.GetValidOtpAsync(request.Email, request.Otp);

                if (otp == null)
                {
                    return new ApiResponse<object>(
                        StatusCodes.BadRequest,
                        ResetPassword.InvalidOtp
                    );
                }

                return new ApiResponse<object>(
                    StatusCodes.Success,
                    ResetPassword.OtpVerifiedSuccessfully
                );
            }
            catch
            {
                return new ApiResponse<object>(
                    StatusCodes.InternalServerError,
                    ResetPassword.InvalidOtp
                );
            }
        }

        public async Task<ApiResponse<object>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                // Verify OTP again
                var otp = await _otpRepo.GetValidOtpAsync(request.Email, request.Otp);

                if (otp == null)
                {
                    return new ApiResponse<object>(
                        StatusCodes.BadRequest,
                        ResetPassword.InvalidOtp
                    );
                }

                // Hash the new password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

                // Update user password
                await _userRepo.UpdatePasswordAsync(request.Email, hashedPassword);

                // Mark OTP as used
                otp.IsUsed = true;
                await _otpRepo.UpdateAsync(otp);

                await tx.CommitAsync();

                return new ApiResponse<object>(
                    StatusCodes.Success,
                    ResetPassword.PasswordResetSuccessfully
                );
            }
            catch
            {
                await tx.RollbackAsync();

                return new ApiResponse<object>(
                    StatusCodes.InternalServerError,
                    ResetPassword.FailedToResetPassword
                );
            }
        }

        public async Task<ApiResponse<object>> GetDebugOtpsAsync(string email)
        {
            try
            {
                var otps = await _otpRepo.GetAllOtpsForEmailAsync(email);
                
                var debugInfo = otps.Select(o => new
                {
                    OtpCode = o.OtpCode,
                    IsUsed = o.IsUsed,
                    ExpiryTime = o.ExpiryTime,
                    CreatedAt = o.CreatedAt,
                    IsExpired = o.ExpiryTime <= DateTime.UtcNow,
                    CurrentTime = DateTime.UtcNow
                }).ToList();

                return new ApiResponse<object>(
                    StatusCodes.Success,
                    "Debug OTP information retrieved",
                    debugInfo
                );
            }
            catch
            {
                return new ApiResponse<object>(
                    StatusCodes.InternalServerError,
                    "Failed to retrieve debug information"
                );
            }
        }

        public async Task<ApiResponse<object>> TestEmailAsync(string email)
        {
            try
            {
                var testOtp = "123456";
                var emailSent = await _emailService.SendOtpEmailAsync(email, testOtp);

                return new ApiResponse<object>(
                    emailSent ? StatusCodes.Success : StatusCodes.InternalServerError,
                    emailSent ? "Test email sent successfully" : "Failed to send test email",
                    new { TestOtp = testOtp, EmailSent = emailSent }
                );
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>(
                    StatusCodes.InternalServerError,
                    $"Error testing email: {ex.Message}"
                );
            }
        }

    }

}