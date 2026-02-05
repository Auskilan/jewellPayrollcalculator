using Microsoft.AspNetCore.Mvc;
using PayrollCalculator.Application.DTOs.Auth;
using PayrollCalculator.Application.DTOs.Common;
using PayrollCalculator.Application.Interfaces.Services;
using AppStatusCodes = PayrollCalculator.Application.Constants.StatusCodes.StatusCodes;




namespace payrollcalculator.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/superadmin/signup
        [HttpPost("superadmin/signup")]
        public async Task<IActionResult> SuperAdminSignup([FromBody] SuperAdminSignupRequest request)
        {
            if (request is null)
                return BadRequest(new ApiResponse<object>(
                    AppStatusCodes.BadRequest,
                    "Request body is required."
                ));

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var response = await _authService.SuperAdminSignupAsync(request);

            return StatusCode(response.StatusCode, response);
        }
        // POST api/auth/login

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        // POST api/auth/forgot-password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (request is null)
                return BadRequest(new ApiResponse<object>(
                    AppStatusCodes.BadRequest,
                    "Request body is required."
                ));

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var response = await _authService.ForgotPasswordAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        // POST api/auth/verify-otp
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            if (request is null)
                return BadRequest(new ApiResponse<object>(
                    AppStatusCodes.BadRequest,
                    "Request body is required."
                ));

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var response = await _authService.VerifyOtpAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        // POST api/auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (request is null)
                return BadRequest(new ApiResponse<object>(
                    AppStatusCodes.BadRequest,
                    "Request body is required."
                ));

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var response = await _authService.ResetPasswordAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        // GET api/auth/debug-otps/{email} - For development only
        [HttpGet("debug-otps/{email}")]
        public async Task<IActionResult> DebugOtps(string email)
        {
            // This endpoint should only be available in development
            if (!Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.Equals("Development", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound();
            }

            var response = await _authService.GetDebugOtpsAsync(email);
            return StatusCode(response.StatusCode, response);
        }

        // POST api/auth/test-email - For development only
        [HttpPost("test-email")]
        public async Task<IActionResult> TestEmail([FromBody] TestEmailRequest request)
        {
            // This endpoint should only be available in development
            if (!Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.Equals("Development", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound();
            }

            var response = await _authService.TestEmailAsync(request.Email);
            return StatusCode(response.StatusCode, response);
        }

        public class TestEmailRequest
        {
            public string Email { get; set; }
        }

    }
}