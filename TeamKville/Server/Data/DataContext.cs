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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }


		public DbSet<Event> Events { get; set; }




	}
}
