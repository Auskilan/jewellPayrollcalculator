using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{

    public class Overtime
    {
        public int OvertimeId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly OvertimeDate { get; set; }
        public decimal Hours { get; set; }
        public string ApprovalStatus { get; set; } = string.Empty;
    }
}
