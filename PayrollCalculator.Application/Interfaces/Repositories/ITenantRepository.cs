using PayrollCalculator.Domain.Entities;

namespace PayrollCalculator.Application.Interfaces.Repositories
{
    public interface ITenantRepository
    {
        Task<Tenant> CreateAsync(Tenant tenant);
        Task<Tenant?> GetByUserIdAsync(int userId);
        Task<Tenant?> GetByIdAsync(int tenantId);
        Task<Tenant> UpdateAsync(Tenant tenant);
    }
}