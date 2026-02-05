using Microsoft.EntityFrameworkCore;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;
using PayrollCalculator.Infrastructure.Persistence;

namespace PayrollCalculator.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly AppDbContext _context;

        public OrganizationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Organization> CreateAsync(Organization organization)
        {
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
            return organization;
        }

        public async Task<Organization?> GetByIdAsync(Guid organizationId)
        {
            return await _context.Organizations
                .FirstOrDefaultAsync(o => o.OrganizationId == organizationId);
        }

        public async Task<Organization> UpdateAsync(Organization organization)
        {
            organization.UpdatedAt = DateTime.UtcNow;
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
            return organization;
        }
    }
}