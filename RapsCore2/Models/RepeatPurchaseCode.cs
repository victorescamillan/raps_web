using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class RepeatPurchaseCode
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string PinCode { get; set; }
        public int? MemberId { get; set; }
        public int? MobileId { get; set; }
        public int? CenterId { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
