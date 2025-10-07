using Microsoft.EntityFrameworkCore;
using MVCproject.Models;


namespace MVCproject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vendor> Vendors { get; set; }

    }
}
