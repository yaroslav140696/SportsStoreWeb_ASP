using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Data.Entity;
using System.Linq;

namespace SportsStore.Domain.Concrete
{
    public class EFWishListRepository : IWishListRepository
    {
        EFDbContext context = new EFDbContext();
        public EFWishListRepository()
        {
            Database.SetInitializer(new MyInitializer());
        }
        public IQueryable<WishListLine> Items
        {
            get
            {
                return context.WishListLines;
            }
        }

        public WishListLine DeleteItem(int itemID)
        {
            var item = context.WishListLines.Include("User").FirstOrDefault(x => x.WishlistlineID == itemID);
            if (item != null)
            {
                var user = item.User;
                user.WishList.Remove(item);
                context.WishListLines.Remove(item);
                context.SaveChanges();
            }
            return item;
        }

        public void SaveItem(int userID, int itemID)
        {
            var product = context.Products.Find(itemID);
            var user = context.Users.Include(x=>x.WishList).FirstOrDefault(x=>x.userID==userID);
            if (user.Email != "null@null")
            {
                var ifItemExist = user.WishList.Find(x => x.WishlistlineID == itemID);
                if (ifItemExist == null)
                {
                    user.WishList.Add(new WishListLine { Product = product });
                    context.SaveChanges();
                }
            }
        }

        public IQueryable<WishListLine> UserWishList(int userID)
        {
            var user = context.Users
                .Include(x => x.WishList)
                .Include(x=>x.WishList.Select(p=>p.Product))
                .FirstOrDefault(x => x.userID == userID);
            return user.WishList.AsQueryable();
        }
    }
}

