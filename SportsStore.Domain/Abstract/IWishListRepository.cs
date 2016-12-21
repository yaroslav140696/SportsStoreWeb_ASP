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

        void SaveItem(int userID, int productID);

        IQueryable<WishListLine> UserWishList(int userID);
     
    }
}
