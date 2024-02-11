using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Raythose.Models
{
    public class MainCategory
    {
        [Key]
        public int MainId { get; set; }

        [Required]
        [MaxLength(100)]
        public string MainCategoryName { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = "active";
    }
}
