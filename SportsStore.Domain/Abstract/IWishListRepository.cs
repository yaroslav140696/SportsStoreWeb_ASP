using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
    public interface IWishListRepository
    {

        IQueryable<WishListLine> Items { get; }

        WishListLine DeleteItem(int itemID);

        void SaveItem(WishListLine item, int productID);

        IQueryable<WishListLine> UserWishList(int userID);
     
    }
}
