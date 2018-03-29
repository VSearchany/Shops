using System;

namespace Shops.Data
{
    public class UnitOfWork: IDisposable
    {
        private ShopContext context = new ShopContext();
        private ShopRepository shopRepository;
        private ProductRepository productRepository;

        public ShopRepository Shops
        {
            get
            {
                if (shopRepository == null)
                    shopRepository = new ShopRepository(context);
                return shopRepository;
            }
        }

        public ProductRepository Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(context);
                return productRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}