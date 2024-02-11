using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Raythose.DB;
using Raythose.Models;
using Rythose.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace Rythose.Controllers
{
    public class CustomerController : Controller
    {
        private readonly SmtpSettings _smtp;
        private readonly ApplicationDbContext ctx;

        public CustomerController(IOptions<SmtpSettings> smtpSettings, ApplicationDbContext db)
        {
            this.ctx = db;
            this._smtp = smtpSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerProfile()
        {
            if (HttpContext.Session.GetString("LoggedIn") == "True")
            {
                string customerIdString = HttpContext.Session.GetString("Cus");
                int customerId = Convert.ToInt32(customerIdString);

                Customer customer = ctx.tbl_customer.FirstOrDefault(c => c.CustomerId == customerId);

                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult UpdateProfile(string fullName, string licenseCard,string contact,string email,string address,string shippingAddres)
        {
            string sql1 = "UPDATE tbl_customer SET FullName = @fullname,Contact = @contact,LicenseCard=@licensecard,Email=@email,Address=@address,ShippingAddres=@shippingAddres WHERE CustomerId = @CustomerId";
            string customerIdString = HttpContext.Session.GetString("Cus");

            ctx.Database.ExecuteSqlRaw(sql1,
                new SqlParameter("@CustomerId", customerIdString),
                new SqlParameter("@fullname", fullName),
                new SqlParameter("@contact", contact),
                new SqlParameter("@licensecard", licenseCard),
                new SqlParameter("@email", email),
                new SqlParameter("@shippingAddres", shippingAddres),
                new SqlParameter("@address", address)
            );

            ctx.SaveChanges();

            return RedirectToAction("CustomerProfile");
        }

        public IActionResult OwnAircrafts()
        {
            string customerIdString = HttpContext.Session.GetString("Cus");

            if (string.IsNullOrEmpty(customerIdString))
            {
                return RedirectToAction("Login", "Customer"); 
            }

            int customerId = Convert.ToInt32(customerIdString);

            // Fetch orders for the current customer
            var orders = ctx.tbl_order
                .Include(o => o.Aircraft)
                .Where(o => o.CustomerId == customerId)
                .Where(o => o.OrderStatus == "Completed")
                .ToList();

            // Now 'orders' contains a list of orders for the current customer
            // Each order has information about the associated aircraft

            return View(orders);
        }
    

    public IActionResult OrderHistory()
        {
            string customerIdString = HttpContext.Session.GetString("Cus");

            if (string.IsNullOrEmpty(customerIdString))
            {
                return RedirectToAction("Login", "Customer");
            }

            int customerId = Convert.ToInt32(customerIdString);

            // Fetch orders for the current customer
            var orders = ctx.tbl_order
                .Include(o => o.Aircraft)
                .Include(item => item.SeatingOption)
                .Include(item => item.InteriorDesign)
                .Include(item => item.ConnectivityOption)
                .Include(item => item.EntertainmentOption)
                .Where(o => o.CustomerId == customerId)
                .Where(o => o.OrderStatus != "Completed")
                .Where(o => o.PaymentStatus == "paid")
                .ToList();

            return View(orders);
        }

        public IActionResult OwnedSingle(int id)
        {
            string customerIdString = HttpContext.Session.GetString("Cus");
            Aircraft singleAircraft = ctx.tbl_aircraft.FirstOrDefault(a => a.AircraftId == id);

            if (string.IsNullOrEmpty(customerIdString))
            {
                return RedirectToAction("Login", "Customer");
            }

            return View(singleAircraft);
        }

        public IActionResult CusList()
        {
            IEnumerable<Customer> objList = ctx.tbl_customer.ToList();
            return View(objList);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register(int id)
        {
            if(id == 1)
            {
                ViewBag.ErrorMessage = "The data you've entered is already registered..!!";
            }
            else
            {
                ViewBag.ErrorMessage = "";
            }
            return View();
        }


        [HttpPost]
        public IActionResult Register(Customer model)
        {
            if (ModelState.IsValid)
            {
                bool isDuplicate = ctx.tbl_customer.Any(c =>
                                c.IdNumber == model.IdNumber ||
                                c.Contact == model.Contact ||
                                c.Email == model.Email ||
                                c.LicenseCard == model.LicenseCard ||
                                c.Username == model.Username);

                try
                {
                    var hashedPassword = GetMd5Hash(model.Password);
                    var verificationCode = GenerateVerificationCode();

                    if (isDuplicate)
                    {
                        return RedirectToAction("Register", "Customer", new { id = 1 });
                    }
                    else {
                        var customer = new Customer
                        {
                            IdNumber = model.IdNumber,
                            FullName = model.FullName,
                            Contact = model.Contact,
                            Email = model.Email,
                            Address = model.Address,
                            City = model.City,
                            Country = model.Country,
                            ZipCode = model.ZipCode,
                            LicenseCard = model.LicenseCard,
                            Username = model.Username,
                            Password = hashedPassword,
                            VerificationCode = verificationCode,
                            Status = "active",
                            ShippingAddres = model.Address + ", "+ model.City + ", " + model.Country
                        };

                        ctx.tbl_customer.Add(customer);
                        ctx.SaveChanges();

                        SendEmail(model.Email, verificationCode);
                        return RedirectToAction("Login", "Customer");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw; 
                }
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult Login_action(string username, string password)
        {
            // Hash the provided password using MD5 (replace with a more secure hashing algorithm)
            string hashedPassword = GetMd5Hash(password);

            var user = ctx.tbl_customer.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);

            if (user != null)
            {
                int customerId = user.CustomerId;
                string customerIdAsString = user?.CustomerId.ToString();
                HttpContext.Session.SetString("Cus", customerIdAsString);
                HttpContext.Session.SetString("LoggedIn", "True");
                return RedirectToAction("Index", "Home");
            }

            // Login failed
            ViewBag.ErrorMessage = "Invalid username or password";
            return RedirectToAction("Login", "Customer");
        }


        private string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        
        private string GenerateVerificationCode()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder verificationCode = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                verificationCode.Append(chars[random.Next(chars.Length)]);
            }

            return verificationCode.ToString();
        }



        [HttpPost]
        public IActionResult SendEmail(string toEmail, string verificationCode)
        {
            try
            {
                using (var client = new SmtpClient(_smtp.SmtpServer, _smtp.Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtp.UserName, _smtp.Password);
                    client.EnableSsl = true;

                    var message = new MailMessage
                    {
                        From = new MailAddress("geshniklamenta@gmail.com"),
                        Subject = "Raythos Account Verification",
                        IsBodyHtml = true,
                        Body = $@"
                        <html>
                            <head>
                                <style>
                                    body {{
                                        font-family: 'Arial', sans-serif;
                                        background-color: #f4f4f4;
                                        color: #333;
                                    }}
                                    .container {{
                                        max-width: 600px;
                                        margin: 0 auto;
                                        padding: 20px;
                                        background-color: #fff;
                                        border-radius: 5px;
                                        box-shadow: 0 0 10px rgba(0,0,0,0.1);
                                    }}
                                    h1 {{
                                        color: #0073E6;
                                    }}
                                </style>
                            </head>
                            <body>
                                <div class='container'>
                                    <h2>Raythos Account Verification</h2>
                                    <p>Your verification code is: <span style='color: #0073E6;font-size:20px;font-weight:bold;'>{verificationCode}</span></p>
                                    <p>Use this code to verify your Raythos account.</p>
                                </div>
                            </body>
                        </html>"
                    };

                    message.To.Add(toEmail);
                    client.Send(message);
                    ViewBag.ErrorMessage = null;
                    return RedirectToAction("Success");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
