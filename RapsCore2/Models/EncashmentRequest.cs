using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class EncashmentRequest
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public int? RequestTypeId { get; set; }
        public DateTime? DateRequested { get; set; }
        public int? UserId { get; set; }
        public bool? IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Net { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Remarks { get; set; }
    }
}
