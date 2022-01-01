using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
