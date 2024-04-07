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
        [Phone]
        public int StaffMember_phonenum { get; set; }

        [ForeignKey("SignedCharityGrps")]
        public int SignedCharityGrpId { get; set; }
        public SignedCharityGrps SignedCharityGrps { get; set; }
    }
}
