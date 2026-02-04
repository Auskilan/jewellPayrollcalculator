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
    }
}
