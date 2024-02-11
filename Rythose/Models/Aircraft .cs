
using System.ComponentModel.DataAnnotations;

namespace Raythose.Models
{
    public class Aircraft
    {
        [Key]
        public int AircraftId { get; set; }

        [Required]
        [MaxLength(150)]
        public string? ModelName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? AircraftType { get; set; }

        [Required]
        public int Passengers { get; set; }

        [Required]
        public int Baggage { get; set; }

        [Required]
        public float CabinWidth { get; set; }

        [Required]
        public float CabinHeight { get; set; }

        [Required]
        public float CabinLength { get; set; }

        [Required]
        public float Range { get; set; }

        [Required]
        public float Speed { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Fuel { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? Description { get; set; }
        public int? Rating { get; set; }

        public float MaxPrice { get; set; }


        [MaxLength(150)]
        public string? FrontImage1 { get; set; }

        [MaxLength(150)]
        public string? FrontImage2 { get; set; }

        [MaxLength(150)]
        public string? FrontImage3 { get; set; }

        [MaxLength(150)]
        public string? InnerImage1 { get; set; }

        [MaxLength(150)]
        public string? InnerImage2 { get; set; }

        [MaxLength(150)]
        public string? InnerImage3 { get; set; }

        [MaxLength(150)]
        public string? SeatingImage { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = "active";

    }
}
