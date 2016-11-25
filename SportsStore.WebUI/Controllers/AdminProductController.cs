using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AdminProductController : Controller, IAdminController<Product>
    {
        public ViewResult Create()
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete()
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(Product item)
        {
            throw new NotImplementedException();
        }

        public ViewResult Edit(int T_id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}