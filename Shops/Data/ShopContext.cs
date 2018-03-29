using System.Data.Entity;
using Shops.Models;

namespace Shops.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("DefaultConnection")
        { }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}