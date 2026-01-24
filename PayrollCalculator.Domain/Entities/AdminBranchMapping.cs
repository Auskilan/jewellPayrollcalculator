using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Domain.Entities
{
    public class AdminBranchMapping
    {
        public int AdminBranchId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
    }
}
