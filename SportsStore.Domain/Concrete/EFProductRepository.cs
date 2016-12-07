using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IRepository<Product>
    {
        EFDbContext context = new EFDbContext();
        public EFProductRepository()
        {
            Database.SetInitializer(new MyInitializer());
        }
        public IQueryable<Product> Items
        {
            get
            {
                return context.Products;
            }
        }

        public Product DeleteItem(int productID)
        {
            var product = context.Products.Find(productID);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public void SaveItem(Product product)
        {
            if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                var dbEntry = context.Products.FirstOrDefault(x => x.ProductID == product.ProductID);
                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Description = product.Description;
                    dbEntry.QuantityInStock = product.QuantityInStock;
                }
            }
            context.SaveChanges();
        }
    }
}
