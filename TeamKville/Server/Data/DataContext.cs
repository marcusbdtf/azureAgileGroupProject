using Microsoft.EntityFrameworkCore;

namespace TeamKville.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
    }
}
