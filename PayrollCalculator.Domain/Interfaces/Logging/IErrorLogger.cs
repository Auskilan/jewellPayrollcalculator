using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Interfaces.Logging
{
    public interface IErrorLogger
    {
        Task LogErrorAsync(Exception exception, string? context = null,
            IDictionary<string, string>? additionalData = null);
    }
}
