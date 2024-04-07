using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AvcolGrpsCharity.Models;
using Microsoft.AspNetCore.Identity;

namespace AvcolGrpsCharity.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AvcolCharitySoftwareUser class
public class RegisteredUsers  :  IdentityUser
{ 
    public int MemberID { get; set; }

    [Required]
    public string Member_name { get; set; }

    [Required]
    [EmailAddress]
    public string Member_email { get; set; }

    [Required(AllowEmptyStrings = true)]
    [Phone]
    public int Member_phonenum { get; set; }

    [Required(AllowEmptyStrings = true)]
    public DateTime DonationDate { get; set; } = DateTime.Now;

    [ForeignKey("Donors")]
    public int DonorID { get; set; }
    public Donors Donors { get; set; }
}