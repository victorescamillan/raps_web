using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class Stockist
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
