using Microsoft.Extensions.DependencyInjection;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Application.Interfaces.Services;
using PayrollCalculator.Infrastructure.Logg;
using PayrollCalculator.Infrastructure.Repositories;
using PayrollCalculator.Infrastructure.Services;
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
            // Existing repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordResetOtpRepository, PasswordResetOtpRepository>();

            // New repositories for shop functionality
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IBranchAddressRepository, BranchAddressRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IShopService, ShopService>();

            // Logging
            services.AddScoped<IErrorLogger, ErrorLogger>();
            services.AddScoped<IAuditLogger, AuditLogger>();

            return services;
        }
    }
}
