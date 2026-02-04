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

    }
}