using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapsCore2.Models.MemberViewModel
{
    public class CommissionDetailsViewModel
    {
        public String Downline { get; set; }
        public String Prog { get; set; }
        public int Level { get; set; }
        public Decimal Amount { get; set; }
        public DateTime Date_Requested{ get; set; }
    }
}
