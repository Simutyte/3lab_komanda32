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

        public DbSet<Business> Businesses => Set<Business>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<OrderConfirmation> OrdersConfirmations => Set<OrderConfirmation>();
        public DbSet<ProductDiscount> ProductDiscounts => Set<ProductDiscount>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<ManagePrivilege> Privileges => Set<ManagePrivilege>();
        public DbSet<LoyaltyProgram> Loyalties => Set<LoyaltyProgram>();
    }
}
