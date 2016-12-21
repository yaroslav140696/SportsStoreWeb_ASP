using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
        }
        // GET: WishList
        public ActionResult Index()
        { 
            return View();
        }

        public void AddItem(CurrentUser user, int productID)
        {
            repository.SaveItem(user.Data.userID, productID);
            TempData["Messege"] = "Продукт добавлен в список желаемых товаров";
        }
        
        public PartialViewResult GetItems(CurrentUser user)
        {
            var list = repository.UserWishList(user.Data.userID);
            return PartialView(list);
        }

        public void DeleteItem(int itemID)
        {
            repository.DeleteItem(itemID);
            TempData["Messege"] = "Продукт удален из списка желаемых товаров";
        }

    }
}