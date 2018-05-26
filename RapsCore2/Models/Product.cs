using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public int? ProductTypeId { get; set; }
        public string Name { get; set; }
        public string Descrip { get; set; }
        public string Tagline { get; set; }
        public string Details { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public int? Qty { get; set; }
        public decimal? MemberPrice { get; set; }
        public decimal? Srp { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
