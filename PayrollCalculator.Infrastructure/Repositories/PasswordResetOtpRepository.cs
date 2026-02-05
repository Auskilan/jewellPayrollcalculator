using Microsoft.EntityFrameworkCore;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;
using PayrollCalculator.Infrastructure.Persistence;

namespace PayrollCalculator.Infrastructure.Repositories
{
    public class PasswordResetOtpRepository : IPasswordResetOtpRepository
    {
        private readonly AppDbContext _context;

        public PasswordResetOtpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PasswordResetOtp> AddAsync(PasswordResetOtp otp)
        {
            _context.PasswordResetOtps.Add(otp);
            await _context.SaveChangesAsync();
            return otp;
        }

        public async Task<PasswordResetOtp?> GetValidOtpAsync(string email, string otpCode)
        {
            // Add logging to debug the issue
            var allOtps = await _context.PasswordResetOtps
                .Where(o => o.Email == email)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            // Log all OTPs for this email for debugging
            foreach (var otp in allOtps)
            {
                Console.WriteLine($"OTP: {otp.OtpCode}, IsUsed: {otp.IsUsed}, ExpiryTime: {otp.ExpiryTime}, CurrentTime: {DateTime.UtcNow}");
            }

            return await _context.PasswordResetOtps
                .FirstOrDefaultAsync(o => 
                    o.Email == email && 
                    o.OtpCode == otpCode && 
                    !o.IsUsed && 
                    o.ExpiryTime > DateTime.UtcNow);
        }

        public async Task UpdateAsync(PasswordResetOtp otp)
        {
            _context.PasswordResetOtps.Update(otp);
            await _context.SaveChangesAsync();
        }

        public async Task InvalidateOtpsForEmailAsync(string email)
        {
            var otps = await _context.PasswordResetOtps
                .Where(o => o.Email == email && !o.IsUsed)
                .ToListAsync();

            foreach (var otp in otps)
            {
                otp.IsUsed = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<PasswordResetOtp>> GetAllOtpsForEmailAsync(string email)
        {
            return await _context.PasswordResetOtps
                .Where(o => o.Email == email)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}