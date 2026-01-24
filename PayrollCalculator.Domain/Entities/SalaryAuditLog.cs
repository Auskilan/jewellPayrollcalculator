using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class SalaryAuditLog
    {
        public int AuditId { get; set; }
        public int SalaryId { get; set; }
        public string Action { get; set; } = string.Empty;
        public int PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}
