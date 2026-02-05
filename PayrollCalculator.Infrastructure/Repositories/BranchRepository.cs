using Microsoft.EntityFrameworkCore;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;
using PayrollCalculator.Infrastructure.Persistence;

namespace PayrollCalculator.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _context;

        public BranchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Branch> CreateAsync(Branch branch)
        {
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<Branch?> GetByTenantIdAsync(int tenantId)
        {
            return await _context.Branches
                .FirstOrDefaultAsync(b => b.TenantId == tenantId && b.IsHeadOffice);
        }

        public async Task<Branch> UpdateAsync(Branch branch)
        {
            _context.Branches.Update(branch);
            await _context.SaveChangesAsync();
            return branch;
        }
    }
}