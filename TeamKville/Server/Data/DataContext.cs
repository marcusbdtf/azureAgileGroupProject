using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;

namespace TeamKville.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
