using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AdminUserController : IAdminController<User>
    {
        public AdminUserController(IRepository<User> repos) : base(repos) { }
        [Authorize]
        public override ActionResult DeleteItem(int itemID)
        {
            var deleteduser = repository.DeleteItem(itemID);
            if (deleteduser != null)
            {
                TempData["Message"] = string.Format("{0} {1} был удален", deleteduser.FirtsName, deleteduser.SecondName);
            }
            return RedirectToAction("Tables", "Admin");
        }

        [HttpPost]
        public override ActionResult Edit(User item)
        {
            if (ModelState.IsValid)
            {
                repository.SaveItem(item);
                TempData["Message"] = string.Format("{0} {1} был изменен", item.FirtsName, item.SecondName);
                return RedirectToAction("Tables", "Admin");
            }
            else return View(item);
        }
        
        public override ActionResult Edit(int itemID)
        {
            var user = repository.Items.FirstOrDefault(x => x.userID == itemID);
            return PartialView(user);
        }

    }
}