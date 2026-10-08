using Microsoft.EntityFrameworkCore;
using IndexWMSProject.Models;

namespace IndexWMSProject.Data
{
    public class WmsDbContext : DbContext
    {
        public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<B2BOrder> Orders { get; set; }
        public DbSet<B2BOrderItem> OrderItems { get; set; }
    }
}
