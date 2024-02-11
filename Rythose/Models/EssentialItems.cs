using Microsoft.EntityFrameworkCore;
using Raythose.DB;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Raythose.Models
{
    public class EssentialItems 
    {
        [Key]
        public int EssentialId { get; set; }

        [MaxLength(20)]
        public string EssentialType { get; set; }

        [MaxLength(100)]
        public string EssentialName { get; set; }

        public int EssentialQuantity { get; set; }

        [MaxLength(20)]
        public string EssentialDate { get; set; }

        public int EssentialStock { get; set; }


    }
}
