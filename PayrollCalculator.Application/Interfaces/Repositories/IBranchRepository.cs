using PayrollCalculator.Domain.Entities;

namespace PayrollCalculator.Application.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch> CreateAsync(Branch branch);
        Task<Branch?> GetByTenantIdAsync(int tenantId);
        Task<Branch> UpdateAsync(Branch branch);
    }
}