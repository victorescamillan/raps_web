using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RapsCore2.Models
{
    public partial class Member
    {
        public int Id { get; set; }
        public int SponsorId { get; set; }
        public int RoleId { get; set; }
        public int RankId { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Mi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }
        public string Beneficiary { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string ContactNo { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsMaintenance { get; set; }
        public DateTime? DateJoin { get; set; }
        public string TinNo { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
