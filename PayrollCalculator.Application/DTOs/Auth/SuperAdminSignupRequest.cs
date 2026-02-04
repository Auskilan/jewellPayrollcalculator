using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.DTOs.Auth
{
    public class SuperAdminSignupRequest
    {
        public string TenantName { get; set; }
        public string SubDomain { get; set; }

        public string BranchName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
