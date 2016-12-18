using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository repository;

        public OrderController(IOrderRepository repos)
        {
            repository = repos;
        }
        public ActionResult Index(int userID = 0)
        {
            
            if (userID == 0)
            {
                var orders = repository.AllOrders;
                return View(orders);
            }
            else
            {
                var orders = repository.GetUserOrders(userID);
                return View(orders);
            }
            
        }
        public ViewResult UserOrders(CurrentUser user)
        {
            var orders = repository.GetUserOrders(user.Data.userID);
            return View(orders);
        }
        [HttpGet]
        public ActionResult DetailInfoAboutOrder(int orderID=1)
        {
            var orderDetais = repository.OrderExtension(orderID);
            return PartialView(orderDetais);
        }
    }
}