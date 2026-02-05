using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class Branch
    {
        public int BranchId { get; set; }
        public int TenantId { get; set; }
        public Guid? OrganizationId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public bool IsHeadOffice { get; set; }
        public bool Isactive { get; set; }

        // Contact information (non-address fields)
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }

        public bool IsCompleted { get; set; }

        // Navigation
        public Tenant Tenant { get; set; } = null!;
        public Organization? Organization { get; set; }
        public ICollection<AdminBranchMapping> AdminBranchMappings { get; set; } = null!;
        public ICollection<BranchAddress> BranchAddresses { get; set; } = new List<BranchAddress>();
    }
}
