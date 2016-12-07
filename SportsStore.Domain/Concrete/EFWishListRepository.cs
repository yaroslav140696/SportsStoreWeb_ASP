using SportsStore.Domain.Entities;
using System.Data.Entity;
using System.Linq;

namespace SportsStore.Domain.Concrete
{
    public class EFWishListRepository
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
            var item = context.WishListLines.Include("User").FirstOrDefault(x=>x.WishlistlineID == itemID);
            if (item != null)
            {
                var user = item.User;
                user.WishList.Remove(item);
                context.WishListLines.Remove(item);
                context.SaveChanges();
            }
            return item;
        }

        public void SaveItem(WishListLine item)
        {
                var user = item.User;
                user.WishList.Add(item);
                context.WishListLines.Add(item);
                context.SaveChanges();
        }

        public IQueryable<WishListLine> UserWishList(int userID)
        {
            var user = context.Users.Include(x=>x.WishList).FirstOrDefault(x=>x.userID == userID);
            return user.WishList.AsQueryable();
        }
    }
}

