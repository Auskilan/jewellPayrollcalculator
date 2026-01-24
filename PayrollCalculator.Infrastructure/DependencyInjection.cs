using Microsoft.Extensions.DependencyInjection;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Infrastructure.Logg;
using PayrollCalculator.Infrastructure.Repositories;
using PayrollCalculator.Domain.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IErrorLogger, ErrorLogger>();
            services.AddScoped<IAuditLogger, AuditLogger>();

            return services;
        }
    }
}
