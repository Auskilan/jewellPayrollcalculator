using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public DateTime SwipeTime { get; set; }
        public int? DeviceId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
