using Microsoft.EntityFrameworkCore;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;
using PayrollCalculator.Infrastructure.Persistence;

namespace PayrollCalculator.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Address> CreateAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> GetByIdAsync(Guid addressId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.AddressId == addressId);
        }
    }
}