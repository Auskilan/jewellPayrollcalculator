using Microsoft.EntityFrameworkCore;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;
using PayrollCalculator.Infrastructure.Persistence;

namespace PayrollCalculator.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tenant> CreateAsync(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return tenant;
        }

        public async Task<Tenant?> GetByUserIdAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Tenant)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            
            return user?.Tenant;
        }

        public async Task<Tenant?> GetByIdAsync(int tenantId)
        {
            return await _context.Tenants
                .FirstOrDefaultAsync(t => t.TenantId == tenantId);
        }

        public async Task<Tenant> UpdateAsync(Tenant tenant)
        {
            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync();
            return tenant;
        }
    }
}