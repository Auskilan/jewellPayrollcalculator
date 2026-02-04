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

        public string BranchName { get; set; } = string.Empty;

        public bool IsHeadOffice { get; set; }
        public bool Isactive { get; set; }

        // NEW COLUMNS
        public string? ShopName { get; set; }
        public string? GstNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }

        public bool IsCompleted { get; set; }   // ⭐

        // Navigation
        public Tenant Tenant { get; set; }
        public ICollection<AdminBranchMapping> AdminBranchMappings { get; set; }
    }

}
