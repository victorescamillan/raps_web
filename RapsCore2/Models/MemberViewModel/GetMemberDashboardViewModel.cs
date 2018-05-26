using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapsCore2.Models.MemberViewModel
{
    public class GetMemberDashboardViewModel
    {
        public int downlines { get; set; }
        public decimal pending_comm { get; set; }
        public decimal last_comm { get; set; }
        public int level1 { get; set; }
    }
}
