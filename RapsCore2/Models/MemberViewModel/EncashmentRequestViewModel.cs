using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapsCore2.Models.MemberViewModel
{
    public class EncashmentRequestViewModel
    {
        public int id { get; set; }
        public int member_id { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string mi { get; set; }
        public string type { get; set; }
        public decimal gross { get; set; }
        public decimal tax { get; set; }
        public decimal net { get; set; }
        public DateTime date_requested{ get; set; }
        public int user_id{ get; set; }
        public bool is_approved { get; set; }
        public int approved_by { get; set; }
        public DateTime date_approved { get; set; }
        public DateTime timestamp { get; set; }
        public string remarks{ get; set; }
    }
}
