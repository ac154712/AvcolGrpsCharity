using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AvcolGrpsCharity.Models
{
    public class CharityCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Category name cannot be longer than 50 characters.")]
        [MinLength(5, ErrorMessage = "Category name must contain atleast 5 characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [Display(Name = "Category")]
        public string Category_name { get; set; } //name

        [ForeignKey("SignedCharityGrps")]
        [Display(Name = "Charity Group")] 
        public int SignedCharityGrpId { get; set; } //foreign key
        public SignedCharityGrps SignedCharityGrps { get; set; } 
    }
}
