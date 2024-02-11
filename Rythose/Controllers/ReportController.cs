using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raythose.DB;
using Raythose.Models;
using Rythose.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Rythose.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext ctx;

        public ReportController(ApplicationDbContext db)
        {
            this.ctx = db;
        }

        public IActionResult Income()
        {
            List<Order> orders = ctx.tbl_order
                .Include(item => item.Aircraft)
                .Include(item => item.Customer)
                .Include(item => item.SeatingOption)
                .Include(item => item.InteriorDesign)
                .Include(item => item.ConnectivityOption)
                .Include(item => item.EntertainmentOption)
                .Where(item => item.OrderStatus == "Completed")
                .ToList();

            return View(orders);
        }

        public IActionResult Order()
        {
            List<Order> orders = ctx.tbl_order
                .Include(item => item.Aircraft)
                .Include(item => item.Customer)
                .Include(item => item.SeatingOption)
                .Include(item => item.InteriorDesign)
                .Include(item => item.ConnectivityOption)
                .Include(item => item.EntertainmentOption)
                .Where(item => item.OrderStatus == "Completed")
                .ToList();

            return View(orders);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
