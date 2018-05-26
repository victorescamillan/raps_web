using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RapsCore2.Models.AdminViewModel
{
    public class EncashmentViewModel
    {
        public int member_id { get; set; }
        public string member_name { get; set; }
        public string request_type { get; set; }
        [DataType(DataType.Date)]
        public DateTime? request_date { get; set; }
        [DataType(DataType.Date)]
        public DateTime? approved_date { get; set; }
        public decimal gross { get; set; }
        public decimal tax { get; set; }
        public decimal net { get; set; }
    }
}
