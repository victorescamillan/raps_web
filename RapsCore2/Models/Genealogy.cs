using System;
using System.Collections.Generic;

namespace RapsCore2.Models
{
    public partial class Genealogy
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int SponsorId { get; set; }
        public int Level { get; set; }
    }
}
