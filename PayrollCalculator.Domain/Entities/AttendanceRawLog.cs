using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class AttendanceRawLog
    {
        public int RawLogId { get; set; }
        public int DeviceId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public DateTime EventTime { get; set; }
        public DateTime SyncedAt { get; set; }
    }
}
