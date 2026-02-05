using PayrollCalculator.Domain.Entities;

namespace PayrollCalculator.Application.Interfaces.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Organization> CreateAsync(Organization organization);
        Task<Organization?> GetByIdAsync(Guid organizationId);
        Task<Organization> UpdateAsync(Organization organization);
    }
}