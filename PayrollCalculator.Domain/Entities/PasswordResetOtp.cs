using System;

namespace PayrollCalculator.Domain.Entities
{
    public class PasswordResetOtp
    {
        public int OtpId { get; set; }
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}