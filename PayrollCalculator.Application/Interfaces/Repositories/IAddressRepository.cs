using PayrollCalculator.Domain.Entities;

namespace PayrollCalculator.Application.Interfaces.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> CreateAsync(Address address);
        Task<Address?> GetByIdAsync(Guid addressId);
    }
}