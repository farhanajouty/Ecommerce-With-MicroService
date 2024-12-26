using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1, // Replace with the actual property name for the primary key
                CouponCode = "10OFF", // Example property
                DiscountAmount = 10, // Example property
                MinAmount = 20,  // Example property
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2, // Replace with the actual property name for the primary key
                CouponCode = "20OFF", // Example property
                DiscountAmount = 20, // Example property
                MinAmount = 40,  // Example property
            });
        }

    }
}
