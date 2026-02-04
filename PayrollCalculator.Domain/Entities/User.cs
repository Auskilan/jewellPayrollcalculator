using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public int TenantId { get; set; }   // FK

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Tenant Tenant { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<AdminBranchMapping> AdminBranchMappings { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
