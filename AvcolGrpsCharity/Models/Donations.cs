using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvcolGrpsCharity.Models
{
    public class Donations
    {
        [Key]
        public int DonationID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")] //only accepts a number with no more than 2 decimals and 18 whole numbers
        //if user enters a number with more than two decimal places, or a number without decimals, the software will automatically correct format (18, 2)
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")] // only accepts positive numbers
        [Display(Name = "Amount")]
        public decimal DonationAmount { get; set; }

        [StringLength(500, ErrorMessage = "Message cannot be longer than 500 characters.")]
        [Display(Name = "Message(optional)")] //using the display annotionation to show its optional
        public string DonationMessage { get; set; } //There is no [Required] annotation to imply that it is optional

        [Required(AllowEmptyStrings = true)] // data field must not be null but user can not input if they want
        //[Range(DateTime.ErrorMessage = "Can't input future dates")]
        [Range(typeof(DateTime), "1/1/2023", "12/31/2029", ErrorMessage = "Invalid date range. Donations are only valid until 2029")]
        public DateTime DonationDate { get; set; } = DateTime.Now; // auto generates to current date and time

        [ForeignKey("Donors")]
        public int DonorID { get; set; }
        public Donors Donors { get; set; } //navigation property
    }
}
