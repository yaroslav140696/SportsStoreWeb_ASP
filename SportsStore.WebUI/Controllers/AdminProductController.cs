using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers.Abstract;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class AdminProductController : IAdminController<Product>
    {
        public AdminProductController(IRepository<Product> repos) : base(repos) { }
        
        public override ActionResult DeleteItem(int itemID)
        {
            var deletedProduct = repository.DeleteItem(itemID);
            if (deletedProduct != null)
            {
                TempData["Message"] = string.Format("{0} был удален", deletedProduct.Name);
            }
            return RedirectToAction("Tables","Admin");
        }

        [HttpPost]
        public override ActionResult Edit(Product item)
        {
            if (ModelState.IsValid)
            {
                repository.SaveItem(item);
                TempData["Message"] = string.Format("{0} был изменен", item.Name);
                return RedirectToAction("Tables","Admin");
            }
            else
            {
                return PartialView(item);
            }
        }

        [HttpGet]
        public override ActionResult Edit(int itemID = 0)
        {
            if (itemID == 0)
            {
                return CreateItem();
            }
            var product = repository.Items.FirstOrDefault(x => x.ProductID == itemID);
            //return Json(new { product = product }, JsonRequestBehavior.AllowGet);
            return PartialView(product);
        }
    }
}