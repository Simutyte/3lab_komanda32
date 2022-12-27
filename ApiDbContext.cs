using _3lab_komanda32.Models;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
        {
        }

        public DbSet<Business> Businesses { get; set; }
    }
}
