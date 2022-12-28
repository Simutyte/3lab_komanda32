using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderConfirmation> OrdersConfirmations { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ManagePrivilege> Privileges { get; set; }
    }
}
