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
        ICartRepository repository;
        IOrderRepository orderRepository;
        public CartController(ICartRepository repos, IOrderRepository orderRepos)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
            repository = repos;
            orderRepository = orderRepos;

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
        public ActionResult IncrementQuantity(int productID)
        {
            repository.AddItem(productID, 1);
            var line = repository.GetUserCart().FirstOrDefault(x => x.Product.ProductID == productID);
            var productsValue = string.Format("{0:c}", line.Product.Price * line.Quantity);
            return Json(new
            {
                totalValue = string.Format("{0:c}", repository.ComputeTotalValue()),
                productsValue = productsValue,
                quantity = line.Product.QuantityInStock,
                totalQuantity = repository.GetUserCart().Sum(x => x.Quantity),
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DecrementQuantity(int productID)
        {
            repository.DecrementQuantity(productID);
            var line = repository.GetUserCart().FirstOrDefault(x => x.Product.ProductID == productID);
            var productsValue = string.Format("{0:c}", line.Product.Price * line.Quantity);
            return Json(new
            {
                totalValue = string.Format("{0:c}", repository.ComputeTotalValue()),
                productsValue,
                quantity = line.Product.QuantityInStock,
                totalQuantity = repository.GetUserCart().Sum(x => x.Quantity),
            }, JsonRequestBehavior.AllowGet);
        }
        public RedirectToRouteResult RemoveFromCart(int productID, string returnUrl)
        {
            repository.RemoveLine(productID);

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(CurrentUser user)
        {
            repository.SetUserId(user.Data.userID);
            return PartialView(repository);
        }
        [HttpPost]
        public ActionResult Checkout(CurrentUser user, ShippingDetails shippingDetails)
        {
            if (repository.GetUserCart().Count() == 0)
            {
                ModelState.AddModelError("", "Ваша корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    orderRepository.AddUserOrder(user.Data.userID, shippingDetails);
                    TempData["Message"] = string.Format("Уважаемый пользователь, ваш заказ успешно обработан, уведомление о заказе выслано на вашу почту:{0}", shippingDetails.Email);
                }
                catch (Exception e)
                {
                    TempData["Message"] = string.Format("Ошибка:{0}", e.Message);
                }

                return RedirectToAction("List", "Product");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        public ViewResult Checkout(CurrentUser user)
        {
            ShippingDetails sd = new ShippingDetails();
            //if (user.Data.Email != "null@null")
            //{
                sd.Email = "yaroslav140696@gmail.com";
                sd.Name = string.Format("{0} {1}", user.Data.FirtsName,user.Data.SecondName);
                sd.Country = "Украина";
                sd.City = "Чернигов";
                sd.Line1  = "Толстого 118/53";
                sd.Zip = "14000";
                sd.GiftWrap = true;
            //}

            return View(sd);
        }
    }
}