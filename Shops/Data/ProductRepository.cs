using Shops.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace Shops.Data
{
    public class ProductRepository : IRepositoty<Product>
    {
        private ShopContext context;

        public ProductRepository(ShopContext context)
        {
            this.context = context;
        }

        public void Create(Product item)
        {
            context.Products.Add(item);
        }

        public void Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
            }
        }

        public Product Get(int id)
        {
            return context.Products.Find(id);
        }

        public IEnumerable<Product> GetList()
        {
            return context.Products;
        }

        public void Update(Product item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}