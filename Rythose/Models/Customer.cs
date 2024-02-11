using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Raythose.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? IdNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Contact { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicenseCard { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Password { get; set; }


        [MaxLength(10)]
        public string? VerificationCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "active";

        [MaxLength(200)]
        public string? ShippingAddres { get; set; }

    }
}
