using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Raythose.DB;
using Raythose.Models;
using Rythose.Models;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Rythose.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly ApplicationDbContext ctx;

        public AdminHomeController(ApplicationDbContext db)
        {
            this.ctx = db;
        }

        public IActionResult Index()
        {
            var orders = ctx.tbl_order.ToList();

            // Get the current year
            int currentYear = DateTime.Now.Year;

            // Hardcoded month names for all 12 months
            var labels = new[]
            {
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    };

            // Group orders by month for the current year
            var monthlyOrderCounts = orders
    .Where(o => !string.IsNullOrEmpty(o.OrderDate) && o.OrderDate.Length >= 10 &&
                DateTime.TryParseExact(o.OrderDate.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                && DateTime.Parse(o.OrderDate.Substring(0, 10)).Year == currentYear)
    .GroupBy(o => DateTime.Parse(o.OrderDate.Substring(0, 10)).Month)
    .Select(g => new { Month = g.Key, Count = g.Count() })
    .OrderBy(x => x.Month)
    .ToList();



            // Convert the data for JavaScript
            var data = new int[12]; // Initialize an array with zeros for all 12 months
            foreach (var entry in monthlyOrderCounts)
            {
                data[entry.Month - 1] = entry.Count; // Subtract 1 to convert month number to array index
            }

            var totalOrders = orders.Count(); 
            var totalIncome = orders
                .Where(o => o.OrderStatus == "Completed" && o.PaymentStatus == "paid")
                .Sum(o => o.FinalAmount); 
            var totalAircrafts = ctx.tbl_aircraft.Count(a => a.Status == "active"); 
            var totalResources = ctx.tbl_items.Sum(item => item.Stock * item.Price); 

            // Pass the data to the view
            ViewData["ChartLabels"] = JsonConvert.SerializeObject(labels);
            ViewData["ChartData"] = JsonConvert.SerializeObject(data);
            ViewData["TotalOrders"] = totalOrders;
            ViewData["TotalIncome"] = totalIncome;
            ViewData["TotalAircrafts"] = totalAircrafts;
            ViewData["TotalResources"] = totalResources;

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        

        private int ParseOrderDate(string orderDate)
        {
            if (DateTime.TryParse(orderDate.Substring(0, 10), out var parsedDate))
            {
                return parsedDate.Month; // Return the month number instead of the year
            }
            return 0; // or handle the case when parsing fails
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
