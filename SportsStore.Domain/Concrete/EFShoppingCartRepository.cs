using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace SportsStore.Domain.Concrete
{
    public class EFShoppingCartRepository : ICartRepository
    {
        EFDbContext context = new EFDbContext();
        static int userID { get; set; }
        public EFShoppingCartRepository()
        {
            Database.SetInitializer(new MyInitializer());
        }
        public IQueryable<CartLine> Items
        {
            get
            {
                return context.CartLines;
            }
        }

        public void AddItem(int productID, int quantity)
        {
            var product = context.Products.Find(productID);
            var user = context.Users
                .Include(x => x.ShoppingCart)
                .Include(x => x.ShoppingCart.Select(o => o.Product))
                .FirstOrDefault(x => x.userID == userID);

            if (product != null)
            {
                CartLine cartline = user.ShoppingCart.Where(x => x.Product.ProductID == productID).FirstOrDefault();
                if (cartline == null)
                {
                    user.ShoppingCart.Add(new CartLine { Product = product, Quantity = quantity });
                }
                else
                    cartline.Quantity += quantity;
            }
            try
            {
                product.QuantityInStock -= quantity;
                context.SaveChanges();
            }
            catch (Exception) { }
        }

        public void DecrementQuantity(int productID)
        {
            var product = context.Products.Find(productID);
            var user = context.Users
                .Include(x => x.ShoppingCart)
                .Include(x => x.ShoppingCart.Select(o => o.Product))
                .FirstOrDefault(x => x.userID == userID);
            if (product != null)
            {
                CartLine cartline = user.ShoppingCart.Where(x => x.Product.ProductID == productID).FirstOrDefault();
                if (cartline.Quantity > 1)
                {
                    cartline.Quantity--;
                }
                try
                {
                    product.QuantityInStock += 1;
                    context.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        public decimal ComputeTotalValue()
        {
            var user = context.Users
               .Include(x => x.ShoppingCart)
               .Include(x => x.ShoppingCart.Select(o => o.Product))
               .FirstOrDefault(x => x.userID == userID);
            return user.ShoppingCart.Sum(x => x.Quantity * x.Product.Price);
        }

        public void RemoveLine(int productID)
        {
            var user = context.Users
               .Include(x => x.ShoppingCart)
               .Include(x => x.ShoppingCart.Select(o => o.Product))
               .FirstOrDefault(x => x.userID == userID);

            var product = context.Products.FirstOrDefault(x => x.ProductID == productID);

            if (product != null)
            {
                try
                {
                    product.QuantityInStock += user.ShoppingCart.FirstOrDefault(x => x.Product.ProductID == product.ProductID).Quantity;
                    context.CartLines.RemoveRange(user.ShoppingCart.Where(x => x.Product.ProductID == product.ProductID).AsEnumerable());
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }

        public void Clear()
        {
            var user = context.Users
               .Include(x => x.ShoppingCart)
               .Include(x => x.ShoppingCart.Select(o => o.Product))
               .FirstOrDefault(x => x.userID == userID);

            try
            {
                context.CartLines.RemoveRange(user.ShoppingCart.AsEnumerable());
                context.SaveChanges();
            }
            catch (Exception e ) { System.Diagnostics.Debug.WriteLine(e.Message); }
        }

        public IQueryable<CartLine> GetUserCart()
        {
            var user = context.Users
                 .Include(x => x.ShoppingCart)
                 .Include(x => x.ShoppingCart.Select(o => o.Product))
                 .FirstOrDefault(x => x.userID == userID);
            return user.ShoppingCart.AsQueryable();
        }

        public void SetUserId(int id)
        {
            userID = id;
        }

        public void EndSession()
        {
            User user = context.Users.Include(x => x.ShoppingCart)
                .Include(x => x.ShoppingCart.Select(o => o.Product))
                .Include(x => x.WishList)
                .Include(x => x.WishList.Select(m => m.Product))
                .FirstOrDefault(x => x.userID == userID);
            if (user != null)
            {
                try
                {
                    foreach (var line in user.ShoppingCart)
                    {
                        var product = context.Products.Find(line.Product.ProductID);
                        product.QuantityInStock += line.Quantity;
                        if (user.Email != "null@null")
                        {
                            var wishListItem = user.WishList.FirstOrDefault(x => x.Product.ProductID == line.Product.ProductID);
                            if (wishListItem == null)
                            {
                                user.WishList.Add(new WishListLine { Product = product });
                            }
                        }
                    }
                    context.CartLines.RemoveRange(user.ShoppingCart.AsEnumerable());
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }
    }
}