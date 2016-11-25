using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public abstract class AdminController : Controller, IAdminController<T>
    {
        protected IProductRepository repository;
        public AdminController(IProductRepository repos)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
            repository = repos;
        }
        public ActionResult Index()
        {
            return View(repository.Products);
        }
        public ViewResult EditProduct(int productID)
        {
            var product = repository.Products.FirstOrDefault(x => x.ProductID == productID);
            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["Message"] = string.Format("{0} был изменен", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
        public ViewResult CreateProduct()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if (deletedProduct != null)
            {
                TempData["Message"] = string.Format("{0} был удален", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}