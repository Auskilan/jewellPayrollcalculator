using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using PayrollCalculator.Domain.Interfaces.Logging;
using System.Net;
using System.Text.Json;

namespace PayrollCalculator.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAuditLogger auditLogger,
            IErrorLogger errorLogger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, auditLogger, errorLogger);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            IAuditLogger auditLogger,
            IErrorLogger errorLogger)
        {
            var traceId = context.TraceIdentifier;

            //  Error Logger (technical)
            await errorLogger.LogErrorAsync(
                    exception,
                    context: $"TraceId={traceId}, Path={context.Request.Path}, Method={context.Request.Method}",
                    additionalData: new Dictionary<string, string>
                    {
                        ["TraceId"] = traceId,
                        ["Path"] = context.Request.Path,
                        ["Method"] = context.Request.Method
                    }
                );

            // 🟠 Audit Logger (business visibility)
            await auditLogger.LogAuditAsync(
                action: "UNHANDLED_EXCEPTION",
                performedBy: "SYSTEM",
                data: new
                {
                    TraceId = traceId,
                    Path = context.Request.Path.Value,
                    Method = context.Request.Method,
                    Message = exception.Message
                }
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                success = false,
                traceId,
                message = _env.IsDevelopment()
                    ? exception.Message
                    : "Unexpected error occurred. Please contact support."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
