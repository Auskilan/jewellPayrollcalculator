namespace PayrollCalculator.Application.DTOs.Shop
{
    public class ShopDetailsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
        public int TenantId { get; set; }
        public int BranchId { get; set; }
    }
}