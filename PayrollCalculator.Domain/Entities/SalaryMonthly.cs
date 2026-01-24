using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class SalaryMonthly
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryMonth { get; set; }
        public int SalaryYear { get; set; }

        public decimal TotalHours { get; set; }
        public decimal DeductionHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal NetSalary { get; set; }
    }
}
