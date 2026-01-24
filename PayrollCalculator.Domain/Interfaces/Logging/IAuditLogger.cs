using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Interfaces.Logging
{

        public interface IAuditLogger
        {
            Task LogAuditAsync(
                string action,
                string performedBy,
                object? data = null,
                IDictionary<string, string>? additionalData = null);
        }
    
}
