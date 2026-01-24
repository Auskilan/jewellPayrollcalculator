using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string SubDomain { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
