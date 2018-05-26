using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapsCore2.Models.MemberViewModel
{
    public class GetPurchaseHistoryViewModel
    {
        public int id { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string mi { get; set; }
        public int product_id { get; set; }
        public string descrip { get; set; }
        public string product_code { get; set; }
        public string pin_code { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public DateTime date { get; set; }
        public DateTime timestamp { get; set; }
        public string purchase_type { get; set; }
    }
}
