using PayrollCalculator.Domain.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.Common.Logging
{
    public class LogManager
    {
        private readonly IErrorLogger _errorLogger;
        private readonly IAuditLogger _auditLogger;

        public LogManager(IErrorLogger errorLogger, IAuditLogger auditLogger)
        {
            _errorLogger = errorLogger;
            _auditLogger = auditLogger;
        }

        public Task LogErrorAsync(Exception exception, string? context = null, IDictionary<string, string>? additionalData = null)
        {
            return _errorLogger.LogErrorAsync(exception, context, additionalData);
        }

        public Task LogAuditAsync(string eventName, string? actor = null, string? context = null, IDictionary<string, string>? additionalData = null)
        {
            return _auditLogger.LogAuditAsync(eventName, actor, context, additionalData);
        }
    }
}
