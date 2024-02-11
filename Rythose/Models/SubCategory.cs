using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Raythose.Models
{
    public class SubCategory
    {
        [Key]
        public int SubId { get; set; }

        [Required]
        public int MainId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubCategoryName { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = "active";

    }
}
