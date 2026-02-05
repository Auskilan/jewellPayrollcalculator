using Microsoft.EntityFrameworkCore;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;
using PayrollCalculator.Infrastructure.Persistence;

namespace PayrollCalculator.Infrastructure.Repositories
{
    public class BranchAddressRepository : IBranchAddressRepository
    {
        private readonly AppDbContext _context;

        public BranchAddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BranchAddress> CreateAsync(BranchAddress branchAddress)
        {
            _context.BranchAddresses.Add(branchAddress);
            await _context.SaveChangesAsync();
            return branchAddress;
        }

        public async Task<BranchAddress?> GetByBranchIdAsync(int branchId)
        {
            return await _context.BranchAddresses
                .Include(ba => ba.Address)
                .FirstOrDefaultAsync(ba => ba.BranchId == branchId);
        }
    }
}