using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers.Abstract
{
    public abstract class IAdminController<T> : Controller
    {
        protected IRepository<T> repository;
        protected IAdminController(IRepository<T> repos)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
            repository = repos;
        }
        [Authorize]
        public ActionResult Index()
        {
            return View(repository.Items);
        }
        public abstract ActionResult Edit(int itemID);

        [HttpPost]
        public abstract ActionResult Edit(T item);

        public ActionResult CreateItem()
        {
            return PartialView("Edit", (T)Activator.CreateInstance(typeof(T)));
        }

        [HttpPost]
        public abstract ActionResult DeleteItem(int itemID);
    }
}
