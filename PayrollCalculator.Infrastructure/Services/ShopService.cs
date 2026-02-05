using PayrollCalculator.Application.DTOs.Shop;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Application.Interfaces.Services;
using PayrollCalculator.Domain.Entities;

namespace PayrollCalculator.Infrastructure.Services
{
    public class ShopService : IShopService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IBranchAddressRepository _branchAddressRepository;
        private readonly IUserRepository _userRepository;

        public ShopService(
            IOrganizationRepository organizationRepository,
            ITenantRepository tenantRepository,
            IBranchRepository branchRepository,
            IAddressRepository addressRepository,
            IBranchAddressRepository branchAddressRepository,
            IUserRepository userRepository)
        {
            _organizationRepository = organizationRepository;
            _tenantRepository = tenantRepository;
            _branchRepository = branchRepository;
            _addressRepository = addressRepository;
            _branchAddressRepository = branchAddressRepository;
            _userRepository = userRepository;
        }

        public async Task<ShopDetailsResponse> SetupShopDetailsAsync(ShopDetailsRequest request, int userId)
        {
            try
            {
                // Get user to check if tenant exists
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new ShopDetailsResponse
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }

                // Check if shop is already set up
                var existingTenant = await _tenantRepository.GetByUserIdAsync(userId);
                if (existingTenant != null)
                {
                    var currentBranch = await _branchRepository.GetByTenantIdAsync(existingTenant.TenantId);
                    if (currentBranch?.IsCompleted == true)
                    {
                        return new ShopDetailsResponse
                        {
                            Success = false,
                            Message = "Shop details already configured"
                        };
                    }
                }

                // Create Organization
                var organization = new Organization
                {
                    OrganizationId = Guid.NewGuid(),
                    OrganizationName = request.ShopName,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    BusinessType = "Retail" // Default value
                };

                var createdOrganization = await _organizationRepository.CreateAsync(organization);

                // Create or Update Tenant
                Tenant tenant;
                if (existingTenant == null)
                {
                    tenant = new Tenant
                    {
                        OrganizationId = createdOrganization.OrganizationId,
                        TenantName = request.ShopName,
                        SubDomain = GenerateSubDomain(request.ShopName),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    };
                    tenant = await _tenantRepository.CreateAsync(tenant);

                    // Update user's TenantId
                    user.TenantId = tenant.TenantId;
                    await _userRepository.UpdateAsync(user);
                }
                else
                {
                    // Update existing tenant with organization
                    existingTenant.OrganizationId = createdOrganization.OrganizationId;
                    existingTenant.UpdatedAt = DateTime.UtcNow;
                    tenant = await _tenantRepository.UpdateAsync(existingTenant);
                }

                // Create Address
                var address = new Address
                {
                    AddressId = Guid.NewGuid(),
                    Line1 = request.Address,
                    City = request.City,
                    State = request.State,
                    Country = "India", // Default
                    Pincode = request.Pincode,
                    CreatedAt = DateTime.UtcNow
                };

                var createdAddress = await _addressRepository.CreateAsync(address);

                // Create or Update Branch
                var existingBranch = await _branchRepository.GetByTenantIdAsync(tenant.TenantId);
                Branch branch;

                if (existingBranch == null)
                {
                    branch = new Branch
                    {
                        TenantId = tenant.TenantId,
                        OrganizationId = createdOrganization.OrganizationId,
                        BranchName = request.ShopName,
                        IsHeadOffice = true,
                        Isactive = true,
                        ContactNumber = request.PhoneNumber,
                        Email = user.Email,
                        IsCompleted = true
                    };
                    branch = await _branchRepository.CreateAsync(branch);
                }
                else
                {
                    existingBranch.BranchName = request.ShopName;
                    existingBranch.OrganizationId = createdOrganization.OrganizationId;
                    existingBranch.ContactNumber = request.PhoneNumber;
                    existingBranch.Email = user.Email;
                    existingBranch.IsCompleted = true;
                    branch = await _branchRepository.UpdateAsync(existingBranch);
                }

                // Create BranchAddress mapping
                var branchAddress = new BranchAddress
                {
                    BranchAddressId = Guid.NewGuid(),
                    BranchId = branch.BranchId,
                    AddressId = createdAddress.AddressId,
                    OrganizationId = createdOrganization.OrganizationId,
                    IsPrimary = true
                };

                await _branchAddressRepository.CreateAsync(branchAddress);

                return new ShopDetailsResponse
                {
                    Success = true,
                    Message = "Shop details saved successfully",
                    OrganizationId = createdOrganization.OrganizationId,
                    TenantId = tenant.TenantId,
                    BranchId = branch.BranchId
                };
            }
            catch (Exception ex)
            {
                return new ShopDetailsResponse
                {
                    Success = false,
                    Message = $"Error setting up shop details: {ex.Message}"
                };
            }
        }

        public async Task<bool> IsShopSetupCompleteAsync(int userId)
        {
            try
            {
                var tenant = await _tenantRepository.GetByUserIdAsync(userId);
                if (tenant == null) return false;

                var branch = await _branchRepository.GetByTenantIdAsync(tenant.TenantId);
                return branch?.IsCompleted == true;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateSubDomain(string shopName)
        {
            // Generate a subdomain from shop name
            var subdomain = shopName.ToLower()
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("_", "");
            
            // Add random suffix to ensure uniqueness
            var random = new Random();
            var suffix = random.Next(1000, 9999);
            
            return $"{subdomain}{suffix}";
        }
    }
}