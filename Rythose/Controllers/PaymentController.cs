using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Raythose.DB;
using Raythose.Models;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;

namespace Rythose.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly StripeSettings _stripeSettings;
        private readonly ApplicationDbContext _dbContext;

        public PaymentsController(IOptions<StripeSettings> stripeSettings, ApplicationDbContext dbContext)
        {
            _stripeSettings = stripeSettings.Value;
            _dbContext = dbContext;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public IActionResult CreateSession(string OrderId, string Amount, int AircraftId)
        {
            // Assuming Amount is the numerical value of the price in USD
            var amountInCents = Convert.ToInt64(double.Parse(Amount) * 100); // Convert to cents

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmount = amountInCents,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Product Name",  // Set the name of your product
                    },
                },
                Quantity = 1,
            },
        },
                Mode = "payment",
                SuccessUrl = "http://localhost:5208/Payment/PaymentSuccess/" + OrderId+"/"+ AircraftId,
                CancelUrl = "http://localhost:5208/",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public ActionResult PaymentSuccess(int id,int air)
        {
            try
            {
                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd");

                string sql1 = "UPDATE tbl_order SET OrderStatus = 'no progress', PaymentStatus='paid', OrderDate = @FormattedDate WHERE OrderId = @OrderId";

                _dbContext.Database.ExecuteSqlRaw(sql1,
                    new SqlParameter("@FormattedDate", formattedDate),
                    new SqlParameter("@OrderId", id)
                );

                string sql2 = "UPDATE tbl_aircraft SET Quantity = (Quantity - 1) WHERE AircraftId = @AircraftId";

                _dbContext.Database.ExecuteSqlRaw(sql2,
                    new SqlParameter("@AircraftId", air)
                );

                _dbContext.SaveChanges();

                return Redirect("http://localhost:5208/Customer/OrderHistory");
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in PaymentSuccess: {ex.Message}");
                return RedirectToAction("Error");  // Redirect to an error page or handle it accordingly
            }
        }

    }

}

