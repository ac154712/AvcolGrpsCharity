using System.ComponentModel.DataAnnotations;

namespace AvcolGrpsCharity.Models
{
    public class SignedCharityGrps
    {
        [Key]
        public int CharityGrpID { get; set; }

        [Required]
        public string ChartyGrp_Name { get; set; }

        [Required]
        public string CharityGrp_description { get; set; }

        [Required]
        [EmailAddress]
        public string CharityGrp_email { get; set; }

        [Required]
        [Phone]
        public string CharityGrp_phone { get; set; }

        public ICollection<Donors> DonorsId { get; set; }
        public ICollection<CharityGrpStaff> CharityGrpStaffId { get; set; }

    }
}
