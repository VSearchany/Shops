using Shops.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace Shops.Data
{
    public class ShopRepository: IRepositoty<Shop>
    {
        private ShopContext context;

        public ShopRepository(ShopContext context)
        {
            this.context = context;
        }

        public void Create(Shop item)
        {
            context.Shops.Add(item);
        }

        public void Delete(int id)
        {
            Shop shop = context.Shops.Find(id);
            if (shop != null)
            {
                context.Shops.Remove(shop);
            }
        }

        public Shop Get(int id)
        {
            return context.Shops.Find(id);
        }

        public IEnumerable<Shop> GetList()
        {
            return context.Shops;
        }

        public void Update(Shop item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}