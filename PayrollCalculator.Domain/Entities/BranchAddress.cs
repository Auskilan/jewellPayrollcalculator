namespace PayrollCalculator.Domain.Entities
{
    public class BranchAddress
    {
        public Guid BranchAddressId { get; set; }
        
        public int BranchId { get; set; }
        public Guid AddressId { get; set; }
        public Guid OrganizationId { get; set; }
        
        public bool IsPrimary { get; set; }

        // Navigation properties
        public Branch Branch { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public Organization Organization { get; set; } = null!;
    }
}