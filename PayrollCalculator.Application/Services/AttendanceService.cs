using PayrollCalculator.Application.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.Services
{
    public class AttendanceService
    {
        private readonly LogManager _logManager;

        public AttendanceService(LogManager logManager)
        {
            _logManager = logManager;
        }

        public async Task MarkAttendance()
        {
            try
            {
                // business logic

                await _logManager.LogAuditAsync(
                    "MarkAttendance",
                    "System",
                    "Attendance",
                    new Dictionary<string, string> { { "message", "Attendance marked successfully" } }
                );
            }
            catch (Exception ex)
            {
                await _logManager.LogErrorAsync(ex, "AttendanceService");
                throw;
            }
        }
    }
}
