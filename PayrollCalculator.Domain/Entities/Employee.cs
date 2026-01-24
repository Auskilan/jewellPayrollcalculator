using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int ExperienceId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;

        public string SalaryType { get; set; } = string.Empty;
        public decimal? HourlyRate { get; set; }
        public decimal? MonthlySalary { get; set; }

        public bool IsActive { get; set; }
    }
}
