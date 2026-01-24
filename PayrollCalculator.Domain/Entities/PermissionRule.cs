using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class PermissionRule
    {
        public int RuleId { get; set; }
        public int TenantId { get; set; }
        public decimal MaxPermissionHours { get; set; }
    }
}
