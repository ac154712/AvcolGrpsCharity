using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvcolGrpsCharity.Models
{
    public class CharityGrpStaff
    {
        [Key]
        public int StaffmemberID { get; set; }

        [Required]
        public string StaffMember_name { get; set; }

        [Required]
        [EmailAddress]
        public string StaffMember_email { get; set; }

        [Required(AllowEmptyStrings = true)]
        [RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format")]
        public string StaffMember_phonenum { get; set; }

        [ForeignKey("SignedCharityGrps")]
        public int SignedCharityGrpId { get; set; }
        public SignedCharityGrps SignedCharityGrps { get; set; }
    }
}
