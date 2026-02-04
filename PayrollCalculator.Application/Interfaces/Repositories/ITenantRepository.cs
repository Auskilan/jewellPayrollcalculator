using PayrollCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.Interfaces.Repositories
{
    public interface ITenantRepository
    {
        Task<Tenant> AddAsync(Tenant tenant);
    }
}
