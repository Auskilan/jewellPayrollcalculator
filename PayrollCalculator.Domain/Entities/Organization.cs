using System.ComponentModel.DataAnnotations;

namespace PayrollCalculator.Domain.Entities
{
    public class Organization
    {
        public Guid OrganizationId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string OrganizationName { get; set; } = string.Empty;
        
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Additional fields for shop details
        public string? BusinessType { get; set; }
        public string? Industry { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? TaxId { get; set; }
        public string? Website { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }

        // Navigation properties
        public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
        public ICollection<BranchAddress> BranchAddresses { get; set; } = new List<BranchAddress>();
    }
}