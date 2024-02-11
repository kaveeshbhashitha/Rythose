using Microsoft.AspNetCore.Mvc.Rendering;
using Raythose.DB;
using System.Collections.Generic;

namespace Raythose.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        public Aircraft Aircraft { get; set; }
        public SubCategory SeatingOption { get; set; }
        public SubCategory InteriorDesign { get; set; }
        public SubCategory ConnectivityOption { get; set; }
        public SubCategory EntertainmentOption { get; set; }
        public SelectList SeatingItemList { get; set; }
        public SelectList EntertainmentItemList { get; set; }
        public SelectList InteriorItemList { get; set; }
        public SelectList ConnectivityItemList { get; set; }

        public SelectList AirframeItemList { get; set; }
        public SelectList PowerplantItemList { get; set; }
        public SelectList AvionicsItemList { get; set; }
        public SelectList MiscellaneousItemList { get; set; }
    }
}

