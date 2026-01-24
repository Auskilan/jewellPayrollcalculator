using PayrollCalculator.Domain.Interfaces.Logging;
using Serilog;
using System.Threading.Tasks;

namespace PayrollCalculator.Infrastructure.Logg
{
    public class AuditLogger : IAuditLogger
    {
        public Task LogAuditAsync(
            string action,
            string performedBy,
            object? data = null,
            IDictionary<string, string>? additionalData = null)
        {
            Log.Information(
                "Audit event. Action: {Action}, PerformedBy: {PerformedBy}, Data: {@Data}",
                action,
                performedBy,
                data
            );

            return Task.CompletedTask;
        }
    }
}
