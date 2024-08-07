﻿using System.ComponentModel.DataAnnotations;

namespace AvcolGrpsCharity.Models
{
    public class SignedCharityGrps
    {
        [Key] //setting the primary key
        public int CharityGrpID { get; set; }

        [Required] // making it so that it cant be null when user creates data
        [StringLength(50, ErrorMessage = "Charity group name cannot be longer than 50 characters.")]
        [MinLength(5, ErrorMessage = "Charity group name must contain atleast 5 characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")] //this makes it so that the field only accepts letters (small/big), spaces, hyphens, singular quote
        [Display(Name = "Charity Group Name")]
        public string ChartyGrp_Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be over 500 characters.")] //sets tthe length of string to only have 500 characters which is roughly 75=120 words
        [Display(Name = "Description")]
        public string CharityGrp_description { get; set; }

        [Required]
        [EmailAddress] //making sure it is of the right format, email address format
        [Display(Name = "Group Email")]
        public string CharityGrp_email { get; set; }

        [Required]
        [RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format")] // using the phone format for this data field
        [Display(Name = "Group Phone Number")]
        public string CharityGrp_phone { get; set; }

        public ICollection<Donors> DonorsId { get; set; } // colecting DonorsId from Donors table (navigation property2)
        public ICollection<CharityGrpStaff> CharityGrpStaffId { get; set; }

    }
}
