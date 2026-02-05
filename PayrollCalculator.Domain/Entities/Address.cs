using System.ComponentModel.DataAnnotations;

namespace PayrollCalculator.Domain.Entities
{
    public class Address
    {
        public Guid AddressId { get; set; }
        
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        
        [StringLength(50)]
        public string City { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string State { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Country { get; set; } = string.Empty;
        
        [StringLength(10)]
        public string Pincode { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<BranchAddress> BranchAddresses { get; set; } = new List<BranchAddress>();
    }
}