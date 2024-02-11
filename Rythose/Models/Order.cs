using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Raythose.DB;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raythose.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int AircraftId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int Seating { get; set; }

        [Required]
        public int Interior { get; set; }

        [Required]
        public int Connectivity { get; set; }

        [Required]
        public int Entertainment { get; set; }

        [Required]
        [MaxLength(20)]
        public string OrderStatus { get; set; } = "no progress"; // default value

        [Required]
        [MaxLength(20)]
        public string PaymentStatus { get; set; } = "pending"; // default value

        [Required]
        public float AircraftPrice { get; set; }

        [Required]
        public float SeatingPrice { get; set; }

        [Required]
        public float InteriorPrice { get; set; }

        [Required]
        public float ConnectivityPrice { get; set; }

        [Required]
        public float EntertainmentPrice { get; set; }

        [Required]
        public float VAT { get; set; }

        [Required]
        public float FinalAmount { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = "active";

        [Required]
        public string OrderDate { get; set; }

        public string? ShippedDate { get; set; }

        public string? ExpectedDate { get; set; }

        public string? ActualDate { get; set; }


        [ForeignKey("AircraftId")]
        public Aircraft Aircraft { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("Seating")]
        public SubCategory SeatingOption { get; set; }

        [ForeignKey("Interior")]
        public SubCategory InteriorDesign { get; set; }

        [ForeignKey("Connectivity")]
        public SubCategory ConnectivityOption { get; set; }

        [ForeignKey("Entertainment")]
        public SubCategory EntertainmentOption { get; set; }

        [ForeignKey("OrderId")]
        public Manufacture Manufacture { get; set; }


    }
}
