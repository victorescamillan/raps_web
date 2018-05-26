using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RapsCore2.Models.MemberViewModel
{
    public class SaveMemberViewModel
    {
        public int Id { get; set; }
        public int SponsorId { get; set; }
        public int RoleId { get; set; }
        public int RankId { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string Fname { get; set; }
        public string Mi { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Beneficiary { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string ContactNo { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        
        public string TinNo { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
            [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int purchase_type { get; set; }
        public int product_id { get; set; }
        [Required]
        public string product_code { get; set; }
        [Required]
        public string pin_code { get; set; }
        public int qty { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public int program_id { get; set; }
        public decimal comm { get; set; }
    }
}
