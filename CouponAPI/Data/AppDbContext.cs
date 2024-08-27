using CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons {get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // skapar initial data?
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
            new Coupon()
            {
                ID = 1,
                Name = "10% off",
                Percent = 10,
                IsActive = true,
            },
            new Coupon()
            {
                ID = 2,
                Name = "15% off",
                Percent = 15,
                IsActive = false,
            },
            new Coupon()
            {
                ID = 3,
                Name = "20% off",
                Percent = 20,
                IsActive = false,
            },
            new Coupon()
            {
                ID = 4,
                Name = "25% off",
                Percent = 25,
                IsActive = true,
            });
        }
    }
}
