using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapsCore2.Models.MemberViewModel
{
    public class MemberList
    {
        public int member_id { get; set; }
        public string role { get; set; }
        public string rank { get; set; }
        public int sponsor_id{ get; set; }
        public string sponsor_lname { get; set; }
        public string sponsor_fname { get; set; }
        public string sponsor_mi { get; set; }
        public bool? is_active { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string mi { get; set; }
        public string is_maintenance { get; set; }
        public DateTime? date_join { get; set; }
        public string role_code { get; set; }
        public string address { get; set; }
        public int contact_no { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string bank_account { get; set; }
        public string tin_no { get; set; }
        
    }
}
