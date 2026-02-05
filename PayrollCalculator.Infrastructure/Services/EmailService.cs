using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PayrollCalculator.Application.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace PayrollCalculator.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<bool> SendOtpEmailAsync(string email, string otpCode)
        {
            try
            {
                // For development/testing, log the OTP
                _logger.LogInformation("Attempting to send OTP {OTP} to email {Email}", otpCode, email);
                
                // Check if SMTP is configured
                var smtpHost = _configuration["Email:SmtpHost"];
                var smtpPort = _configuration.GetValue<int>("Email:SmtpPort", 587);
                var smtpUsername = _configuration["Email:SmtpUsername"];
                var smtpPassword = _configuration["Email:SmtpPassword"];
                var fromEmail = _configuration["Email:FromEmail"];
                var fromName = _configuration["Email:FromName"];

                _logger.LogInformation("SMTP Configuration - Host: {Host}, Port: {Port}, Username: {Username}", 
                    smtpHost, smtpPort, smtpUsername);

                // If SMTP is not configured, just log and return true for development
                if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUsername))
                {
                    _logger.LogWarning("SMTP not configured. OTP would be sent to {Email}: {OTP}", email, otpCode);
                    _logger.LogWarning("To enable email sending, configure Email settings in appsettings.json");
                    return true; // Return true for development
                }

                // Create and configure SMTP client
                using var client = new SmtpClient(smtpHost, smtpPort);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Timeout = 30000; // 30 seconds timeout

                // Create email message
                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail ?? smtpUsername, fromName ?? "Payroll Calculator"),
                    Subject = "Password Reset OTP - Payroll Calculator",
                    Body = CreateOtpEmailBody(otpCode),
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                _logger.LogInformation("Sending email via SMTP...");
                
                // Send the email
                await client.SendMailAsync(mailMessage);
                
                _logger.LogInformation("OTP email sent successfully to {Email}", email);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP error while sending OTP email to {Email}. StatusCode: {StatusCode}", 
                    email, smtpEx.StatusCode);
                _logger.LogWarning("OTP for manual testing: {OTP}", otpCode);
                return false; // Return false for SMTP errors
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while sending OTP email to {Email}", email);
                _logger.LogWarning("OTP for manual testing: {OTP}", otpCode);
                return false; // Return false for other errors
            }
        }

        private string CreateOtpEmailBody(string otpCode)
        {
            return $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #333;'>Password Reset Request</h2>
                        <p>You have requested to reset your password. Please use the following OTP to proceed:</p>
                        
                        <div style='background-color: #f8f9fa; padding: 20px; text-align: center; margin: 20px 0; border-radius: 5px;'>
                            <h1 style='color: #007bff; font-size: 32px; margin: 0; letter-spacing: 5px;'>{otpCode}</h1>
                        </div>
                        
                        <p><strong>This OTP will expire in 10 minutes.</strong></p>
                        
                        <p>If you did not request this password reset, please ignore this email.</p>
                        
                        <hr style='margin: 30px 0;'>
                        <p style='color: #666; font-size: 12px;'>
                            This is an automated message from Payroll Calculator. Please do not reply to this email.
                        </p>
                    </div>
                </body>
                </html>";
        }
    }
}