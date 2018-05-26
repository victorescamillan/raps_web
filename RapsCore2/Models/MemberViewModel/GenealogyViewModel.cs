using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapsCore2.Models.MemberViewModel
{
    public class GenealogyViewModel
    {
        public int member_id { get; set; }
        public string member_lname { get; set; }
        public string member_fname { get; set; }
        public string member_mi { get; set; }
        public int downline_id { get; set; }
        public string downline_lname { get; set; }
        public string downline_fname { get; set; }
        public string downline_mi { get; set; }
        public int level { get; set; }
    }
}
