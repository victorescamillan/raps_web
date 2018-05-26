using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class Commission
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int SponsorId { get; set; }
        public int ProgramId { get; set; }
        public int EncashId { get; set; }
        public int Level { get; set; }
        public decimal Amount { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateRequested { get; set; }
        public bool? IsRequested { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsClaimed { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
