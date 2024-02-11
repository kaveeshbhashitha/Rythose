using Microsoft.EntityFrameworkCore;
using Raythose.Models;

namespace Raythose.DB
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Aircraft> tbl_aircraft { get; set; }
        public DbSet<Customer> tbl_customer { get; set; }
        public DbSet<EssentialItems> tbl_essential_items { get; set; }
        public DbSet<Item> tbl_items { get; set; }
        public DbSet<MainCategory> tbl_main_category { get; set; }
        public DbSet<SubCategory> tbl_sub_category { get; set; }
        public DbSet<Order> tbl_order { get; set; }
        public DbSet<Manufacture> tbl_manufacture { get; set; }
        public DbSet<User> tbl_user { get; set; }
    }
}

