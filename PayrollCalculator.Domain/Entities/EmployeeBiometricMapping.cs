using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class EmployeeBiometricMapping
    {
        public int MappingId { get; set; }
        public int EmployeeId { get; set; }
        public int DeviceId { get; set; }

        public string? FaceId { get; set; }
        public string? FingerprintId { get; set; }
        public string? CardId { get; set; }
    }
}
