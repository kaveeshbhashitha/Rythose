using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Raythose.DB;
using Raythose.Models;
using Rythose.Models;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace Rythose.Controllers
{
    public class AircraftController : Controller
    {
        private readonly ApplicationDbContext ctx;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AircraftController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            ctx = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult AddAircraft()
        {
            return View();
        }
        public IActionResult EditAircraft(int id)
        {
            Aircraft aircraft = ctx.tbl_aircraft.Find(id);
            if (aircraft == null)
            {
                return NotFound(); 
            }

            return View(aircraft);
        }

        [HttpPost]
        public IActionResult Register_air(IFormCollection form, IFormFile FrontImage1, IFormFile FrontImage2, IFormFile FrontImage3,IFormFile InnerImage1,IFormFile InnerImage2,IFormFile InnerImage3,IFormFile SeatingImage)
        {
            string fileName1 = FrontImage1?.FileName;
            string fileName2 = FrontImage2?.FileName;
            string fileName3 = FrontImage3?.FileName;
            string fileName4 = InnerImage1?.FileName;
            string fileName5 = InnerImage2?.FileName;
            string fileName6 = InnerImage3?.FileName;
            string fileName7 = SeatingImage?.FileName;

            var model = new Aircraft
            {
                ModelName = form["ModelName"],
                AircraftType = form["AircraftType"],
                Passengers = Convert.ToInt32(form["Passengers"]),
                Baggage = Convert.ToInt32(form["Baggage"]),
                CabinWidth = Convert.ToSingle(form["CabinWidth"]),
                CabinHeight = Convert.ToSingle(form["CabinHeight"]),
                CabinLength = Convert.ToSingle(form["CabinLength"]),
                Range = Convert.ToSingle(form["Range"]),
                Speed = Convert.ToSingle(form["Speed"]),
                Fuel = form["Fuel"],
                Price = Convert.ToSingle(form["Price"]),
                MaxPrice = Convert.ToSingle(form["MaxPrice"]),
                Quantity = Convert.ToInt32(form["Quantity"]),
                Description = form["Description"],
                FrontImage1 = fileName1,
                FrontImage2 = fileName2,
                FrontImage3 = fileName3,
                InnerImage1 = fileName4,
                InnerImage2 = fileName5,
                InnerImage3 = fileName6,
                SeatingImage = fileName7,
                Status = "active"
            };

            if (ModelState.IsValid)
            {
                ctx.tbl_aircraft.Add(model);
                ctx.SaveChanges();

                HttpContext.Session.SetString("Success", "Aircraft Registered Successfully");

                string uploadsDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsDirectory);

                // Save each file to the uploads directory or a subdirectory within it
                SaveFile(FrontImage1, fileName1, uploadsDirectory);
                SaveFile(FrontImage2, fileName2, uploadsDirectory);
                SaveFile(FrontImage3, fileName3, uploadsDirectory);
                SaveFile(InnerImage1, fileName4, uploadsDirectory);
                SaveFile(InnerImage2, fileName5, uploadsDirectory);
                SaveFile(InnerImage3, fileName6, uploadsDirectory);
                SaveFile(SeatingImage, fileName7, uploadsDirectory);

                return RedirectToAction("AircraftList");
            }

            return RedirectToAction("AddAircraft");
        }


        [HttpPost]
        public IActionResult UpdateAircraft(int AircraftId, string modelName, string aircraftType, int passengers, int baggage, float cabinWidth, float cabinHeight, float cabinLength, float range, float speed, string fuel, float price, float maxPrice, int quantity, string description)
        {
            var model = ctx.tbl_aircraft.FirstOrDefault(a => a.AircraftId == AircraftId);

            if (model != null)
            {
                // Check and update properties
                model.ModelName = modelName;
                model.AircraftType = aircraftType;
                model.Passengers = passengers;
                model.Baggage = baggage;
                model.CabinWidth = cabinWidth;
                model.CabinHeight = cabinHeight;
                model.CabinLength = cabinLength;
                model.Range = range;
                model.Speed = speed;
                model.Fuel = fuel;
                model.Price = price;
                model.MaxPrice = maxPrice;
                model.Quantity = quantity;
                model.Description = description;
                model.Status = "active";


                if (ModelState.IsValid)
                {
                    // Save changes to the database using a written query
                    string updateQuery = @"
                UPDATE tbl_aircraft
                SET ModelName = @ModelName,
                    AircraftType = @AircraftType,
                    Passengers = @Passengers,
                    Baggage = @Baggage,
                    CabinWidth = @CabinWidth,
                    CabinHeight = @CabinHeight,
                    CabinLength = @CabinLength,
                    Range = @Range,
                    Speed = @Speed,
                    Fuel = @Fuel,
                    Price = @Price,
                    MaxPrice = @MaxPrice,
                    Quantity = @Quantity,
                    Description = @Description
                WHERE AircraftId = @AircraftId";

                    ctx.Database.ExecuteSqlRaw(updateQuery,
                        new SqlParameter("@ModelName", model.ModelName),
                        new SqlParameter("@AircraftType", model.AircraftType),
                        new SqlParameter("@Passengers", model.Passengers),
                        new SqlParameter("@Baggage", model.Baggage),
                        new SqlParameter("@CabinWidth", model.CabinWidth),
                        new SqlParameter("@CabinHeight", model.CabinHeight),
                        new SqlParameter("@CabinLength", model.CabinLength),
                        new SqlParameter("@Range", model.Range),
                        new SqlParameter("@Speed", model.Speed),
                        new SqlParameter("@Fuel", model.Fuel),
                        new SqlParameter("@Price", model.Price),
                        new SqlParameter("@MaxPrice", model.MaxPrice),
                        new SqlParameter("@Quantity", model.Quantity),
                        new SqlParameter("@Description", model.Description),
                        new SqlParameter("@AircraftId", model.AircraftId)
                    );

                    HttpContext.Session.SetString("Success", "Aircraft Updated Successfully");
                    return RedirectToAction("AircraftList");
                }
            }

            // Existing code for redirecting in case of ModelState not being valid
            return RedirectToAction("AircraftList");
        }



        private void SaveFile(IFormFile file, string fileName, string directoryPath)
        {
            if (file != null && file.Length > 0)
            {
                // Combine the directory path with the file name
                string filePath = Path.Combine(directoryPath, fileName);

                // Save the file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
        }


        public IActionResult AircraftList()
        {
            IEnumerable<Aircraft> objList = ctx.tbl_aircraft.Where(a => a.Status == "active").ToList();
            return View(objList);
        }

        public IActionResult CheckBuy(int id)
        {
            ViewBag.Aircraft = ctx.tbl_aircraft.FirstOrDefault(a => a.AircraftId == id);
            ViewBag.MainCategories = ctx.tbl_main_category.ToList();
            ViewBag.SubCategories = ctx.tbl_sub_category.ToList();
            return View();

        }

        
        public IActionResult ListView()
        {
            IEnumerable<Aircraft> objList = ctx.tbl_aircraft.Where(a => a.Status == "active").ToList();
            return View(objList);
        }

        [HttpPost]
        public IActionResult ListView(string aircraftType, string passengers, string range, string speed)
            {
                IEnumerable<Aircraft> filteredList = ctx.tbl_aircraft.ToList();
            Console.WriteLine($"AircraftType: {aircraftType}, Passengers: {passengers}, Range: {range}, Speed: {speed}");


            if (!string.IsNullOrEmpty(aircraftType))
                {
                    filteredList = filteredList.Where(a => a.AircraftType == aircraftType);
                }

            if (!string.IsNullOrEmpty(passengers))
            {
                int passengersValue = int.Parse(passengers);

                if (passengersValue == 1)
                {
                    // Passenger value between 1-5
                    filteredList = filteredList.Where(a => a.Passengers >= 1 && a.Passengers < 6);
                }
                else if (passengersValue == 2)
                {
                    // Passenger value between 5-10
                    filteredList = filteredList.Where(a => a.Passengers >= 5 && a.Passengers < 11);
                }
                else if (passengersValue == 3)
                {
                    // Passenger value between 10-15
                    filteredList = filteredList.Where(a => a.Passengers >= 10 && a.Passengers < 16);
                }
                else if (passengersValue == 4)
                {
                    // Passenger value between 15-20
                    filteredList = filteredList.Where(a => a.Passengers >= 15 && a.Passengers < 21);
                }
            }


            if (!string.IsNullOrEmpty(range))
                {
                    int rangeValue = int.Parse(range);
                    filteredList = filteredList.Where(a => a.Range == rangeValue);
                if (rangeValue == 1)
                {
                    // range value below 5
                    filteredList = filteredList.Where(a => a.Range < 6);
                }
                else if (rangeValue == 2)
                {
                    // range value between 5-10
                    filteredList = filteredList.Where(a => a.Range >= 5 && a.Range < 11);
                }
                else if (rangeValue == 3)
                {
                    // range value more than 10
                    filteredList = filteredList.Where(a => a.Range > 10);
                }
            }

                if (!string.IsNullOrEmpty(speed))
                {
                
                    int speedValue = int.Parse(speed);
                    if (speedValue == 1)
                    {
                    // speed value below 300
                    filteredList = filteredList.Where(a => a.Speed < 300);
                    }
                    else if (speedValue == 2)
                    {
                    // speed value between 300 to 400
                    filteredList = filteredList.Where(a => a.Speed >= 300 && a.Speed < 401);
                    }
                    else if (speedValue == 3)
                    {
                    // speed value between 400 to 500
                    filteredList = filteredList.Where(a => a.Speed >= 400 && a.Speed < 501);
                    }
                    else if (speedValue == 4)
                    {
                    // ranspeedge value more than 500
                    filteredList = filteredList.Where(a => a.Speed > 500);
                    }
                }

            
            return View(filteredList);
        }
        

        public IActionResult SingleDetail(int id)
        {
            Aircraft singleAircraft = ctx.tbl_aircraft.FirstOrDefault(a => a.AircraftId == id);

            if (singleAircraft == null)
            {
                return NotFound();
            }

            return View(singleAircraft);
        }



        [HttpPost]
        public IActionResult RemoveAircraft(int id)
        {
            try
            {
                string sql1 = "UPDATE tbl_aircraft SET Status = 'no' WHERE AircraftId = @AircraftId";

                ctx.Database.ExecuteSqlRaw(sql1, new SqlParameter("@AircraftId", id));

                ctx.SaveChanges();
                HttpContext.Session.SetString("Success", "Aircraft Successfully Removed");

                return RedirectToAction("AircraftList");
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        public IActionResult RateAircraft(int aircraftId)
        {
            // Update the rating in the database
            var aircraft = ctx.tbl_aircraft.Find(aircraftId);
            if (aircraft != null)
            {
                aircraft.Rating += 1;
                ctx.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }


        public IActionResult CustomerPreference()
        {
            IEnumerable<Aircraft> aircraftList = ctx.tbl_aircraft.Where(a => a.Status == "active").ToList();

            var labels = aircraftList.Select(a => a.ModelName).ToArray();
            var ratings = aircraftList.Select(a => a.Rating).ToArray();

            ViewBag.ChartLabels = labels;
            ViewBag.ChartData = ratings;

            return View(aircraftList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
