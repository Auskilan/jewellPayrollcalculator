namespace PayrollCalculator.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendOtpEmailAsync(string email, string otpCode);
    }
}