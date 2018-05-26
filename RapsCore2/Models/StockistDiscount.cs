using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class StockistDiscount
    {
        public int Id { get; set; }
        public decimal? MobileMembership { get; set; }
        public decimal? MobileRepeatPurchase { get; set; }
        public decimal? CenterMembership { get; set; }
        public decimal? CenterRepeatPurchase { get; set; }
    }
}
