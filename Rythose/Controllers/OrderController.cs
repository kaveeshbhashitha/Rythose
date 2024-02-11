using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Raythose.DB;
using Raythose.Models;
using Rythose.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using SqlParameter = Microsoft.Data.SqlClient.SqlParameter;
using System.Linq;
using System.Net.NetworkInformation;

namespace Rythose.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext ctx;

        public OrderController(ApplicationDbContext db)
        {
            this.ctx = db;
        }

        public IActionResult order_list()
        {
            List<Order> orders = ctx.tbl_order
            .Include(item => item.Aircraft)
            .Include(item => item.Customer)
            .Include(item => item.SeatingOption)
            .Include(item => item.InteriorDesign)
            .Include(item => item.ConnectivityOption)
            .Include(item => item.EntertainmentOption)
            .Where(item => item.OrderStatus == "no progress" && item.PaymentStatus == "Paid")
            .ToList();

            return View(orders);
        }

        public IActionResult start_manufacture(int id)
        {
            Order orderItem = ctx.tbl_order
        .Include(item => item.Aircraft)
        .Include(item => item.Customer)
        .Include(item => item.SeatingOption)
        .Include(item => item.InteriorDesign)
        .Include(item => item.ConnectivityOption)
        .Include(item => item.EntertainmentOption)
        .FirstOrDefault(item => item.OrderId == id);

            if (orderItem == null)
            {
                return NotFound();
            }

            // Fetch the lists of items from tbl_items for different categories
            List<Item> seatingItemList = ctx.tbl_items
                .Where(item => item.SubId == orderItem.Seating && item.Stock > 0)
                .ToList();

            List<Item> entertainmentItemList = ctx.tbl_items
                .Where(item => item.SubId == orderItem.Entertainment && item.Stock > 0)
                .ToList();

            List<Item> interiorItemList = ctx.tbl_items
                .Where(item => item.SubId == orderItem.Interior && item.Stock > 0)
                .ToList();

            List<Item> connectivityItemList = ctx.tbl_items
                .Where(item => item.SubId == orderItem.Connectivity && item.Stock > 0)
                .ToList();



            // Fetch the lists of essential items for different types
            List<EssentialItems> airframeItemList = ctx.tbl_essential_items
                .Where(item => item.EssentialType == "Airframe" && item.EssentialStock > 0)
                .ToList();
            List<EssentialItems> powerplantItemList = ctx.tbl_essential_items
                .Where(item => item.EssentialType == "Powerplant" && item.EssentialStock > 0)
                .ToList();
            List<EssentialItems> avionicsItemList = ctx.tbl_essential_items
                .Where(item => item.EssentialType == "Avionics" && item.EssentialStock > 0)
                .ToList();
            List<EssentialItems> miscellaneousItemList = ctx.tbl_essential_items
                .Where(item => item.EssentialType == "Miscellaneous" && item.EssentialStock > 0)
                .ToList();


            // Create an instance of the view model and set its properties
            OrderViewModel viewModel = new OrderViewModel
            {
                Order = orderItem,
                Aircraft = orderItem.Aircraft,
                SeatingOption = orderItem.SeatingOption,
                InteriorDesign = orderItem.InteriorDesign,
                ConnectivityOption = orderItem.ConnectivityOption,
                EntertainmentOption = orderItem.EntertainmentOption,
                SeatingItemList = new SelectList(seatingItemList, "ItemId", "ItemName"),
                EntertainmentItemList = new SelectList(entertainmentItemList, "ItemId", "ItemName"),
                InteriorItemList = new SelectList(interiorItemList, "ItemId", "ItemName"),
                ConnectivityItemList = new SelectList(connectivityItemList, "ItemId", "ItemName"),
                AirframeItemList = new SelectList(airframeItemList, "EssentialId", "EssentialName"),
                PowerplantItemList = new SelectList(powerplantItemList, "EssentialId", "EssentialName"),
                AvionicsItemList = new SelectList(avionicsItemList, "EssentialId", "EssentialName"),
                MiscellaneousItemList = new SelectList(miscellaneousItemList, "EssentialId", "EssentialName")
            };


            // Pass the view model to the view
            return View(viewModel);
        }



        public IActionResult manufacture_list()
        {
            List<Order> orders = ctx.tbl_order
                .Include(item => item.Aircraft)
                .Include(item => item.Customer)
                .Select(item => new
                {
                    Order = item,
                    Manufacture = ctx.tbl_manufacture
                        .Where(m => m.OrderId == item.OrderId)
                        .FirstOrDefault()
                })
                .Where(item => item.Order.OrderStatus == "Manufacturing")
                .Select(item => new Order
                {
                    Aircraft = item.Order.Aircraft,
                    Customer = item.Order.Customer,
                    Manufacture = item.Manufacture
                })
                .ToList();

            return View(orders);
        }




        public IActionResult start_shipment(int id)
        {
            Order orderItem = ctx.tbl_order
                .Include(item => item.Aircraft)
                .Include(item => item.Customer)
                .FirstOrDefault(item => item.OrderId == id);

            return View(orderItem);
        }

        public IActionResult shipping_list()
        {
            List<Order> orders = ctx.tbl_order
            .Include(item => item.Aircraft)
            .Include(item => item.Customer)
            .Where(item =>  item.OrderStatus == "Dispatched" || item.OrderStatus == "Departed" || item.OrderStatus == "Local")
            .ToList();

            return View(orders);
        }

        public IActionResult completed_list()
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

        public IActionResult Invoice(int id)
        {
            var order = ctx.tbl_order
            .Where(item => item.OrderId == id)
            .GroupJoin(
                ctx.tbl_manufacture,
                order => order.OrderId,
                manufacture => manufacture.OrderId,
                (order, manufactures) => new { Order = order, Manufactures = manufactures })
            .SelectMany(
                result => result.Manufactures.DefaultIfEmpty(),
                (result, manufacture) => new
                {
                    Order = result.Order,
                    Aircraft = result.Order.Aircraft,
                    Customer = result.Order.Customer,
                    Manufacture = manufacture
                })
            .FirstOrDefault();


            if (order == null)
            {
                // Handle the case where no order with the specified id is found
                return NotFound();
            }

            return View(order);
        }

        public ActionResult submit_manufacture(string orderDate, string[] interiorMaterials, string[] entertainmentOptions, string seatingOption, string[] connectivityOptions, int[] airframe, int[] powerplant, int[] avionics, int[] miscellaneousComponents, int orderId)
        {
            string sql1 = "UPDATE tbl_order SET OrderStatus = 'Manufacturing' WHERE OrderId = @OrderId";
            ctx.Database.ExecuteSqlRaw(sql1, new SqlParameter("@OrderId", orderId));

            UpdateStockForItems(interiorMaterials);
            UpdateStockForItems(entertainmentOptions);
            UpdateStockForItem(seatingOption);
            UpdateStockForItems(connectivityOptions);

            UpdateEssentialStockForItems(airframe);
            UpdateEssentialStockForItems(powerplant);
            UpdateEssentialStockForItems(avionics);
            UpdateEssentialStockForItems(miscellaneousComponents);

            var seatingItemName = ctx.tbl_items
                .Where(item => item.ItemId == int.Parse(seatingOption))
                .Select(item => item.ItemName)
                .FirstOrDefault();

            var interiorItemNames = ctx.tbl_items
                .Where(item => interiorMaterials.Contains(item.ItemId.ToString()))
                .Select(item => item.ItemName)
                .ToList();

            var connectivityItemNames = ctx.tbl_items
                .Where(item => connectivityOptions.Contains(item.ItemId.ToString()))
                .Select(item => item.ItemName)
                .ToList();

            var entertainmentItemNames = ctx.tbl_items
                .Where(item => entertainmentOptions.Contains(item.ItemId.ToString()))
                .Select(item => item.ItemName)
                .ToList();


            var airframeNames = ctx.tbl_essential_items
                .Where(item => airframe.Contains(item.EssentialId))
                .Select(item => item.EssentialName)
                .ToList();

            var powerplantNames = ctx.tbl_essential_items
                .Where(item => powerplant.Contains(item.EssentialId))
                .Select(item => item.EssentialName)
                .ToList();

            var avionicsNames = ctx.tbl_essential_items
                .Where(item => avionics.Contains(item.EssentialId))
                .Select(item => item.EssentialName)
                .ToList();

            var miscellaneousComponentNames = ctx.tbl_essential_items
                .Where(item => miscellaneousComponents.Contains(item.EssentialId))
                .Select(item => item.EssentialName)
                .ToList();

            string insertSql = @"
                INSERT INTO tbl_manufacture (OrderId, Date, Seating, Interior, Connectivity, Entertainment, Airframe, Powerplant, Avionics, Miscellaneous)
                VALUES (@OrderId, @Date, @Seating, @Interior, @Connectivity, @Entertainment, @Airframe, @Powerplant, @Avionics, @Miscellaneous)
            ";

            ctx.Database.ExecuteSqlRaw(insertSql,
                new Microsoft.Data.SqlClient.SqlParameter("@OrderId", orderId),
                new Microsoft.Data.SqlClient.SqlParameter("@Date", orderDate),
                new Microsoft.Data.SqlClient.SqlParameter("@Seating", seatingItemName),
                new Microsoft.Data.SqlClient.SqlParameter("@Interior", string.Join(",", interiorItemNames)),
                new Microsoft.Data.SqlClient.SqlParameter("@Connectivity", string.Join(",", connectivityItemNames)),
                new Microsoft.Data.SqlClient.SqlParameter("@Entertainment", string.Join(",", entertainmentItemNames)),
                new Microsoft.Data.SqlClient.SqlParameter("@Airframe", string.Join(",", airframeNames)),
                new Microsoft.Data.SqlClient.SqlParameter("@Powerplant", string.Join(",", powerplantNames)),
                new Microsoft.Data.SqlClient.SqlParameter("@Avionics", string.Join(",", avionicsNames)),
                new Microsoft.Data.SqlClient.SqlParameter("@Miscellaneous", string.Join(",", miscellaneousComponentNames))
            );

            ctx.SaveChanges();

            return RedirectToAction("order_list");
        }

        public ActionResult submit_shipping(string ShippedDate, string ExpectedDate, int orderId)
        {
            string sql1 = "UPDATE tbl_order SET OrderStatus = 'Dispatched', ShippedDate = @ShippedDate, ExpectedDate = @ExpectedDate WHERE OrderId = @OrderId";

            ctx.Database.ExecuteSqlRaw(sql1,
                new SqlParameter("@ShippedDate", ShippedDate),
                new SqlParameter("@ExpectedDate", ExpectedDate),
                new SqlParameter("@OrderId", orderId)
            );

            ctx.SaveChanges();
            return RedirectToAction("manufacture_list");
        }


        public ActionResult ToDepart(int id)
        {
            string sql1 = "UPDATE tbl_order SET OrderStatus = 'Departed' WHERE OrderId = @OrderId";
            ctx.Database.ExecuteSqlRaw(sql1, new SqlParameter("@OrderId", id));

            ctx.SaveChanges();

            return RedirectToAction("shipping_list");
        }

        public ActionResult ToLocal(int id)
        {
            string sql1 = "UPDATE tbl_order SET OrderStatus = 'Local' WHERE OrderId = @OrderId";
            ctx.Database.ExecuteSqlRaw(sql1, new SqlParameter("@OrderId", id));

            ctx.SaveChanges();

            return RedirectToAction("shipping_list");
        }

        public ActionResult finish_shipping(int id)
        {
            string formattedDate = DateTime.Now.ToString("yyyy-MM-dd");
            string sql1 = "UPDATE tbl_order SET OrderStatus = 'Completed', ActualDate = @FormattedDate WHERE OrderId = @OrderId";

            ctx.Database.ExecuteSqlRaw(sql1,
                new SqlParameter("@FormattedDate", formattedDate),
                new SqlParameter("@OrderId", id)
            );

            ctx.SaveChanges();

            return RedirectToAction("shipping_list");
        }


        private void UpdateStockForItem(string itemId)
        {
            string updateSql = "UPDATE tbl_items SET Stock = (Stock - 1) WHERE ItemId = @ItemId";
            ctx.Database.ExecuteSqlRaw(updateSql, new SqlParameter("@ItemId", itemId));
        }

        private void UpdateStockForItems(IEnumerable<string> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                UpdateStockForItem(itemId);
            }
        }

        private void UpdateEssentialStockForItems(int[] essentialIds)
        {
            foreach (var essentialId in essentialIds)
            {
                string updateSql = "UPDATE tbl_essential_items SET EssentialStock = (EssentialStock - 1) WHERE EssentialId = @EssentialId";
                ctx.Database.ExecuteSqlRaw(updateSql, new SqlParameter("@EssentialId", essentialId));
            }
        }





        //main order process customer end
        public ActionResult temp_order(int seatOpt, int intOpt, int conOpt, int entOpt, int AircraftId)
        {
            var aircraft = ctx.tbl_aircraft.FirstOrDefault(a => a.AircraftId == AircraftId);

            var seatingPrice = ctx.tbl_sub_category.FirstOrDefault(s => s.SubId == seatOpt)?.Price ?? 0;
            var interiorPrice = ctx.tbl_sub_category.FirstOrDefault(s => s.SubId == intOpt)?.Price ?? 0;
            var connectivityPrice = ctx.tbl_sub_category.FirstOrDefault(s => s.SubId == conOpt)?.Price ?? 0;
            var entertainmentPrice = ctx.tbl_sub_category.FirstOrDefault(s => s.SubId == entOpt)?.Price ?? 0;


            decimal vatRate = 0.15m; // 15%
            decimal aircraftPriceDecimal = (decimal)(aircraft?.Price ?? 0);
            decimal seatingPriceDecimal = (decimal)(seatingPrice);
            decimal interiorPriceDecimal = (decimal)(interiorPrice);
            decimal connectivityPriceDecimal = (decimal)(connectivityPrice);
            decimal entertainmentPriceDecimal = (decimal)(entertainmentPrice);
            string customerIdString = HttpContext.Session.GetString("Cus");
            int customerId = Convert.ToInt32(customerIdString);

            decimal sumOfPrices = (decimal)(aircraftPriceDecimal + seatingPriceDecimal + interiorPriceDecimal + connectivityPriceDecimal + entertainmentPriceDecimal);
            decimal vat = sumOfPrices * vatRate;
            decimal finalAmount = sumOfPrices + vat;

            // Insert into tbl_order using Entity Framework
            string Sql = $@"
                    INSERT INTO tbl_order (
                        AircraftId,
                        CustomerId,
                        Seating,
                        Interior,
                        Connectivity,
                        Entertainment,
                        OrderStatus,
                        PaymentStatus,
                        AircraftPrice,
                        SeatingPrice,
                        InteriorPrice,
                        ConnectivityPrice,
                        EntertainmentPrice,
                        VAT,
                        FinalAmount,
                        Status,
                        OrderDate,
                        ShippedDate,
                        ExpectedDate,
                        ActualDate
                    )
                    VALUES (
                        {AircraftId},
                        {customerId},
                        {seatOpt},
                        {intOpt},
                        {conOpt},
                        {entOpt},
                        'temp',
                        'pending',
                        {(aircraft?.Price ?? 0)},
                        {seatingPrice},
                        {interiorPrice},
                        {connectivityPrice},
                        {entertainmentPrice},
                        {(float)vat},
                        {(float)finalAmount},
                        'active',
                        '0000-00-00',
                        '0000-00-00',
                        '0000-00-00',
                        '0000-00-00'
                    )";

                ctx.Database.ExecuteSqlRaw(Sql);
                ctx.SaveChanges();

            int orderId = ctx.tbl_order.Max(o => o.OrderId);
            return RedirectToAction("Checkout", new { id = orderId });

        }



        public IActionResult Checkout(int id)
        {
            Order orderItem = ctx.tbl_order
            .Include(item => item.Aircraft)
            .FirstOrDefault(item => item.OrderId == id);
            ViewBag.SubCategories = ctx.tbl_sub_category.ToList();
            return View(orderItem);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
