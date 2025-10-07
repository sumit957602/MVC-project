using Microsoft.EntityFrameworkCore;

namespace MVCproject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Vendor> Vendor { get; set; }
        
        }
}
