using PayrollCalculator.Domain.Entities;

namespace PayrollCalculator.Application.Interfaces.Repositories
{
    public interface IBranchAddressRepository
    {
        Task<BranchAddress> CreateAsync(BranchAddress branchAddress);
        Task<BranchAddress?> GetByBranchIdAsync(int branchId);
    }
}