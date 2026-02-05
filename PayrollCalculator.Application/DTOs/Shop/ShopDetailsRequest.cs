using System.ComponentModel.DataAnnotations;

namespace PayrollCalculator.Application.DTOs.Shop
{
    public class ShopDetailsRequest
    {
        [Required(ErrorMessage = "Shop name is required")]
        [StringLength(100, ErrorMessage = "Shop name cannot exceed 100 characters")]
        public string ShopName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pincode is required")]
        [StringLength(10, ErrorMessage = "Pincode cannot exceed 10 characters")]
        public string Pincode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}