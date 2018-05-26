using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int? ProductId { get; set; }
        public int? PurchaseTypeId { get; set; }
        public string ProductCode { get; set; }
        public string PinCode { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? Date { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
