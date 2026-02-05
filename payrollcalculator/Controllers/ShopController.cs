using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollCalculator.Application.DTOs.Shop;
using PayrollCalculator.Application.Interfaces.Services;
using System.Security.Claims;

namespace payrollcalculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost("setup")]
        public async Task<IActionResult> SetupShopDetails([FromBody] ShopDetailsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Invalid user token");
            }

            var result = await _shopService.SetupShopDetailsAsync(request, userId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("setup-status")]
        public async Task<IActionResult> GetSetupStatus()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Invalid user token");
            }

            var isComplete = await _shopService.IsShopSetupCompleteAsync(userId);

            return Ok(new { IsSetupComplete = isComplete });
        }
    }
}