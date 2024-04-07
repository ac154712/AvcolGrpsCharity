using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AvcolGrpsCharity.Models
{
    public class CharityCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string Category_name { get; set; }

        [ForeignKey("SignedCharityGrps")]
        public int SignedCharityGrpId { get; set; }
        public SignedCharityGrps SignedCharityGrps { get; set; }
    }
}
