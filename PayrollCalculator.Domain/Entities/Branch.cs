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
    }
}
