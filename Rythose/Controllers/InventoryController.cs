using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Raythose.DB;
using Raythose.Models;
using Rythose.Models;
using System.Diagnostics;

namespace Rythose.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext ctx;

        public InventoryController(ApplicationDbContext db)
        {
            this.ctx = db;
        }

        public IActionResult CatReg()
        {
            // Fetch the list of MainCategory from the database
            var mainCategories = ctx.tbl_main_category
                .Where(mc => mc.Status == "active")
                .ToList();

            // Pass the list of MainCategory to the view
            return View(mainCategories);
        }

        [HttpPost]
        public IActionResult Register_cat(SubCategory subCategory)
        {
            // Check for duplicates
            var existingSubCategory = ctx.tbl_sub_category
                .FirstOrDefault(x => x.MainId == subCategory.MainId && x.SubCategoryName == subCategory.SubCategoryName);

            if (existingSubCategory != null)
            {
                HttpContext.Session.SetString("Error", "This Subcategory Already Registered.");
                return RedirectToAction("CatReg");
            }

            else
            {
                ctx.tbl_sub_category.Add(subCategory);
                ctx.SaveChanges();

                HttpContext.Session.SetString("Success", "The Subcategory is Registered Successfully");
                return RedirectToAction("CatReg");
            }

        }


        public IActionResult ItemReg()
        {
            ViewBag.MainCategories = ctx.tbl_main_category.Where(mc => mc.Status == "active").ToList();
            ViewBag.SubCategories = ctx.tbl_sub_category.Where(sc => sc.Status == "active").ToList();

            return View();
        }


        [HttpPost]
        public IActionResult Register_item(IFormCollection form)
        {
            // Extract values based on input names
            string itemName = form["ItemName"];
            string date = form["Date"];
            int mainId = Convert.ToInt32(form["MainId"]);
            int subId = Convert.ToInt32(form["SubId"]);
            int quantity = Convert.ToInt32(form["Quantity"]);
            int price = Convert.ToInt32(form["Price"]);
            string vendor = form["Vendor"];

            Item item = new Item
            {
                ItemName = itemName,
                Date = date,
                MainId = mainId,
                SubId = subId,
                Quantity = quantity,
                Price = price,
                Vendor = vendor,
                Stock = quantity
            };

            if (ModelState.IsValid)
            {
                ctx.tbl_items.Add(item);
                ctx.SaveChanges();

                HttpContext.Session.SetString("Success", "The Subcategory is Registered Successfully");
                return RedirectToAction("ItemList");
            }

            return RedirectToAction("ItemReg");
        }

    

    public IActionResult ItemList()
        {
            IEnumerable<Item> objItemList = ctx.tbl_items
            .Include(item => item.MainCategory)
            .Include(item => item.SubCategory)
            .ToList();

            return View(objItemList);
        }

        public IActionResult CoreItemReg()
        {
            return View();
        }

        public async Task<IActionResult> CoreItemList()
        {
            IEnumerable<EssentialItems> objList = ctx.tbl_essential_items.ToList();
            return View(objList);
        }


        [HttpPost]
        public IActionResult CoreRegister_item(IFormCollection form)
        {
            // Extract values based on input names
            string EssentialName = form["EssentialName"];
            string EssentialDate = form["EssentialDate"];
            string EssentialType = form["EssentialType"];
            int EssentialQuantity = Convert.ToInt32(form["EssentialQuantity"]);

            EssentialItems item = new EssentialItems
            {
                EssentialName = EssentialName,
                EssentialDate = EssentialDate,
                EssentialType = EssentialType,
                EssentialQuantity = EssentialQuantity,
                EssentialStock = EssentialQuantity
            };

            if (ModelState.IsValid)
            {
                ctx.tbl_essential_items.Add(item);
                ctx.SaveChanges();

                HttpContext.Session.SetString("Success", "The Item is Registered Successfully");
                return RedirectToAction("CoreItemList");
            }

            return RedirectToAction("CoreItemReg");
        }



        public IActionResult CatList()
        {
            ViewBag.MainCategories = ctx.tbl_main_category.Where(c => c.Status == "active").ToList();
            ViewBag.SubCategories = ctx.tbl_sub_category.Where(c => c.Status == "active").ToList();
            return View();
        }


        [HttpPost]
        public IActionResult UpdateMainCategory(int mainCategoryId, string mainCategoryName)
        {
            string sql1 = "UPDATE tbl_main_category SET MainCategoryName = @name  WHERE MainId = @MainId";

            ctx.Database.ExecuteSqlRaw(sql1,
                new SqlParameter("@MainId", mainCategoryId),
                new SqlParameter("@name", mainCategoryName)
            );

            ctx.SaveChanges();
            HttpContext.Session.SetString("Success", "Main Category Name is Updated Successfully");

            return RedirectToAction("CatList");
        }

        [HttpPost]
        public IActionResult RemoveMainCategory(int id)
        {
            string sql1 = "UPDATE tbl_main_category SET Status = 'no'  WHERE MainId = @MainId";
            string sql2 = "UPDATE tbl_sub_category SET Status = 'no'  WHERE MainId = @MainId";

            ctx.Database.ExecuteSqlRaw(sql1,
                new SqlParameter("@MainId", id)
            );
            ctx.Database.ExecuteSqlRaw(sql2,
                new SqlParameter("@MainId", id)
            );

            ctx.SaveChanges();
            HttpContext.Session.SetString("Success", "Main Category Successfully Removed");

            return RedirectToAction("CatList");
        }


        [HttpPost]
        public IActionResult UpdateSubCategory(int subCategoryId, string subCategoryName, int price)
        {
            string sql1 = "UPDATE tbl_sub_category SET SubCategoryName = @name, Price= @price  WHERE SubId = @SubId";

            ctx.Database.ExecuteSqlRaw(sql1,
                new SqlParameter("@SubId", subCategoryId),
                new SqlParameter("@name", subCategoryName),
                new SqlParameter("@price", price)
            );

            ctx.SaveChanges();
            HttpContext.Session.SetString("Success", "Sub Category Data Updated Successfully");

            return RedirectToAction("CatList");
        }

        [HttpPost]
        public IActionResult RemoveSubCategory(int id)
        {
            string sql2 = "UPDATE tbl_sub_category SET Status = 'no'  WHERE SubId = @SubId";

            ctx.Database.ExecuteSqlRaw(sql2,
                new SqlParameter("@SubId", id)
            );

            ctx.SaveChanges();
            HttpContext.Session.SetString("Success", "Sub Category Successfully Removed");

            return RedirectToAction("CatList");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
