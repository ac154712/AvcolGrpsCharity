using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvcolGrpsCharity.Models
{
    public class Donations
    {
        [Key]
        public int DonationID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DonationAmount { get; set; }

        public string DonationMessage { get; set; }

        [Required(AllowEmptyStrings = true)]
        public DateTime DonationDate { get; set; } = DateTime.Now;

        [ForeignKey("Donors")]
        public int DonorID { get; set; }
        public Donors Donors { get; set; }
    }
}
