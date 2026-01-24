using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class AttendanceCorrection
    {
        public int CorrectionId { get; set; }
        public int AttendanceId { get; set; }
        public int CorrectedBy { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime CorrectedAt { get; set; }
    }
}
