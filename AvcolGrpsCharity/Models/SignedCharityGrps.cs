using System.ComponentModel.DataAnnotations;

namespace AvcolGrpsCharity.Models
{
    public class SignedCharityGrps
    {
        [Key] //setting the primary key
        public int CharityGrpID { get; set; }

        [Required] // making it so that it cant be null
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string ChartyGrp_Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be over 500 characters.")]
        public string CharityGrp_description { get; set; }

        [Required]
        [EmailAddress] //making sure it is of the right format, specifically email address
        public string CharityGrp_email { get; set; }

        [Required]
        [RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format")] // using the phone format for this data field
        public string CharityGrp_phone { get; set; }

        public ICollection<Donors> DonorsId { get; set; } // colecting DonorsId from Donors table
        public ICollection<CharityGrpStaff> CharityGrpStaffId { get; set; }

    }
}
