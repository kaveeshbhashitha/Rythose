
using System.ComponentModel.DataAnnotations;

namespace Raythose.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(10)]
        public string UserType { get; set; }

        [MaxLength(10)]
        public string StaffName { get; set; }



    }
}
