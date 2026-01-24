using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class SalaryRule
    {
        public int RuleId { get; set; }
        public int TenantId { get; set; }
        public int GraceMinutes { get; set; }
        public decimal HalfDayHours { get; set; }
        public decimal FullDayHours { get; set; }
    }
}
