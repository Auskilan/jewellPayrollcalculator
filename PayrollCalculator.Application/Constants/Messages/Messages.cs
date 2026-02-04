using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.Constants.Messages
{
    public static class Signup
    {
        public const string SuperAdminCreatedSuccessfully =
            "Super admin account created successfully";

        public const string EmailAlreadyExists =
            "Email already exists";

        public const string TenantAlreadyExists =
            "Tenant with this subdomain already exists";

        public const string SignupFailed =
            "Signup failed. Please try again later";
    }
}
