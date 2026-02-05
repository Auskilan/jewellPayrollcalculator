using PayrollCalculator.Application.DTOs.Shop;

namespace PayrollCalculator.Application.Interfaces.Services
{
    public interface IShopService
    {
        Task<ShopDetailsResponse> SetupShopDetailsAsync(ShopDetailsRequest request, int userId);
        Task<bool> IsShopSetupCompleteAsync(int userId);
    }
}