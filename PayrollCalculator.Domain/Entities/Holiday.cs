using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class Holiday
    {
        public int HolidayId { get; set; }
        public int TenantId { get; set; }
        public DateOnly HolidayDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
