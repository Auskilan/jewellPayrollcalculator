using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{

    public class EmployeeExperience
    {
        public int ExperienceId { get; set; }
        public int YearsOfExperience { get; set; }
        public string ExperienceLabel { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
    }
}
