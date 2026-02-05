using PayrollCalculator.Domain.Entities;

namespace PayrollCalculator.Application.Interfaces.Repositories
{
    public interface IPasswordResetOtpRepository
    {
        Task<PasswordResetOtp> AddAsync(PasswordResetOtp otp);
        Task<PasswordResetOtp?> GetValidOtpAsync(string email, string otpCode);
        Task UpdateAsync(PasswordResetOtp otp);
        Task InvalidateOtpsForEmailAsync(string email);
        Task<List<PasswordResetOtp>> GetAllOtpsForEmailAsync(string email);
    }
}