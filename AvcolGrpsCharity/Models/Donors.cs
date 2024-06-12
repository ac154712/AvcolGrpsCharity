using AvcolGrpsCharity.Areas.Identity.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvcolGrpsCharity.Models
{
    public class Donors
    {
        [Key]
        public int DonorID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [MinLength(2, ErrorMessage = "Name must containt atleast 2 charaters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [Display(Name = "Name")]
        public string Donor_name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Donor_email { get; set; }

        [ForeignKey("SignedCharityGrps")]
        public int SignedCharityGrpId { get; set; }
        public SignedCharityGrps SignedCharityGrps { get; set; }

        public ICollection<Donations> Donations { get; set; } //referening this line of code in Donations model: public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<RegisteredUsers> RegistersUsers { get; set; }
    }
}
