using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;
using PayrollCalculator.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Infrastructure.Repositories
{
    public class AdminBranchMappingRepository : IAdminBranchMappingRepository
    {
        private readonly AppDbContext _context;

        public AdminBranchMappingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AdminBranchMapping mapping)
        {
            _context.AdminBranchMappings.Add(mapping);
            await _context.SaveChangesAsync();
        }
    }
}
