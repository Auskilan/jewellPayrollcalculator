using PayrollCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task <User?> GetByEmailAsync(string email);
    }
}
