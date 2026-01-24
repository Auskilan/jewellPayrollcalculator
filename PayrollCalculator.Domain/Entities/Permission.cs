using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly PermissionDate { get; set; }
        public decimal Hours { get; set; }
        public string ApprovalStatus { get; set; } = string.Empty;
    }
}
