using PayrollCalculator.Application.DTOs.Auth;
using PayrollCalculator.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> SuperAdminSignupAsync(SuperAdminSignupRequest request);
        Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request);
        Task<ApiResponse<object>> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<ApiResponse<object>> VerifyOtpAsync(VerifyOtpRequest request);
        Task<ApiResponse<object>> ResetPasswordAsync(ResetPasswordRequest request);
        Task<ApiResponse<object>> GetDebugOtpsAsync(string email);
        Task<ApiResponse<object>> TestEmailAsync(string email);
    }
}
