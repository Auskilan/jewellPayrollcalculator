using PayrollCalculator.Domain.Interfaces.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollCalculator.Infrastructure.Logg
{
    public class ErrorLogger : IErrorLogger
    {
        public Task LogErrorAsync(
            Exception exception,
            string? context = null,
            IDictionary<string, string>? additionalData = null)
        {
            if (exception == null)
                return Task.CompletedTask;

            Log.Error(
                exception,
                "Unhandled error occurred. Context: {Context}, AdditionalData: " +
                "{@AdditionalData}",
                context,
                additionalData
            );

            return Task.CompletedTask;
        }
    }
}
