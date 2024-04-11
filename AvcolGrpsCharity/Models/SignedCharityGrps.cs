using System.ComponentModel.DataAnnotations;

namespace AvcolGrpsCharity.Models
{
    public class SignedCharityGrps
    {
        [Key] //setting the primary key
        public int CharityGrpID { get; set; }

        [Required] // making it so that it cant be null
        public string ChartyGrp_Name { get; set; }

        [Required]
        public string CharityGrp_description { get; set; }

        [Required]
        [EmailAddress] //making sure it is of the right format, specifically email address
        public string CharityGrp_email { get; set; }

        [Required]
        [Phone] // using the phone format for this data field
        public string CharityGrp_phone { get; set; }

        public ICollection<Donors> DonorsId { get; set; } // colecting DonorsId from Donors table
        public ICollection<CharityGrpStaff> CharityGrpStaffId { get; set; }

    }
}
