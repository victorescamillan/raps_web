using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class PurchaseType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
