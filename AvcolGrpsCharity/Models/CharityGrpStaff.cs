using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvcolGrpsCharity.Models
{
    public class CharityGrpStaff
    {
        [Key]
        public int StaffmemberID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [MinLength(2, ErrorMessage = "Name must contain atleast 2 characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [Display(Name = "Name: ")]
        public string StaffMember_name { get; set; }

        [Required]
        [Display(Name = "Email: ")]
        [EmailAddress]
        public string StaffMember_email { get; set; }

        [Phone]
        [Required(AllowEmptyStrings = true)]
        [RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format (+64- 10 numbers]):")]
        [Display(Name = "Phone Number: ")]
        public string StaffMember_phonenum { get; set; }

        [ForeignKey("SignedCharityGrps")]
        public int SignedCharityGrpId { get; set; }
        public SignedCharityGrps SignedCharityGrps { get; set; }
    }
}
