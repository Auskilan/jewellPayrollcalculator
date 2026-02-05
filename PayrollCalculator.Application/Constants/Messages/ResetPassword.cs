namespace PayrollCalculator.Application.Constants.Messages
{
    public static class ResetPassword
    {
        public const string OtpSentSuccessfully = "OTP sent to your email successfully";
        public const string EmailNotFound = "Email address not found";
        public const string OtpVerifiedSuccessfully = "OTP verified successfully";
        public const string InvalidOtp = "Invalid or expired OTP";
        public const string PasswordResetSuccessfully = "Password reset successfully";
        public const string OtpExpired = "OTP has expired";
        public const string OtpAlreadyUsed = "OTP has already been used";
        public const string FailedToSendOtp = "Failed to send OTP. Please try again";
        public const string FailedToResetPassword = "Failed to reset password. Please try again";
    }
}