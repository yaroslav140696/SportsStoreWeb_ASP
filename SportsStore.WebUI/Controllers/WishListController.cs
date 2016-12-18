using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class WishListController : Controller
    {
        IWishListRepository repository;
        public WishListController(IWishListRepository repos)
        {
            repository = repos;
        }
        // GET: WishList
        public ActionResult Index()
        { 
            return View();
        }

        public void AddItem(CurrentUser user, int productID)
        {
            WishListLine line = new WishListLine { User = user.Data };
            repository.SaveItem(line, productID);
        }
        
        public PartialViewResult GetItems(CurrentUser user)
        {
            var list = repository.UserWishList(user.Data.userID);
            return PartialView(list);
        }

        public void DeleteItem(int itemID)
        {
            repository.DeleteItem(itemID);
        }

    }
}