using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class Approval
    {
        public int ApprovalId { get; set; }

        // Example values: "Leave", "Permission", "Overtime", "AttendanceCorrection"
        public string ReferenceType { get; set; } = string.Empty;

        // 해당 reference table primary key
        public int ReferenceId { get; set; }

        // User who approved
        public int ApprovedBy { get; set; }

        // Example values: Pending, Approved, Rejected
        public string Status { get; set; } = string.Empty;

        public DateTime ApprovedAt { get; set; }
    }
}
