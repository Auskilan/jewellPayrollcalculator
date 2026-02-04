using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }

        public User User { get; set; }
    }
}
