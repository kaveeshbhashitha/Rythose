
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raythose.Models
{
    public class Manufacture
    {
        [Key]
        public int ManufactureId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Date { get; set; }

        [MaxLength(200)]
        public string Seating { get; set; }

        [MaxLength(200)]
        public string Interior { get; set; }

        [MaxLength(200)]
        public string Connectivity { get; set; }

        [MaxLength(200)]
        public string Entertainment { get; set; }

        [MaxLength(200)]
        public string Airframe { get; set; }

        [MaxLength(200)]
        public string Powerplant { get; set; }

        [MaxLength(200)]
        public string Avionics { get; set; }

        [MaxLength(200)]
        public string Miscellaneous { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
