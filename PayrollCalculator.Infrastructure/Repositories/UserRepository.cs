using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await Task.FromResult(_context.Users.Any(u => u.Email == email));
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return await Task.FromResult(user);
        }

        public async Task UpdatePasswordAsync(string email, string newPasswordHash)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.PasswordHash = newPasswordHash;
                await _context.SaveChangesAsync();
            }
        }
    }

}
