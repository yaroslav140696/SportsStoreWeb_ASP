using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System.Threading;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {

        IOrderProcessor orderProcessor;
        ICartRepository repository;
        public CartController(IOrderProcessor proc, ICartRepository repos)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
            repository = repos;
            orderProcessor = proc;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = repository, ReturnUrl = returnUrl });
        }
        public RedirectToRouteResult AddToCart(int productID, string returnUrl)
        {

            repository.AddItem(productID, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productID, string returnUrl)
        {
            repository.RemoveLine(productID);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult DecrementQuantity(int productID, string returnUrl)
        {

            repository.DecrementQuantity(productID);

            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(CurrentUser user)
        {
            repository.SetUserId(user.Data.userID);
            return PartialView(repository);
        }
        [HttpPost]
        public ViewResult Checkout(CurrentUser user, ShippingDetails shippingDetails)
        {
            if (repository.GetUserCart().Count() == 0)
            {
                ModelState.AddModelError("", "Ваша корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessorOrder(shippingDetails, user);
                repository.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}