using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AdminUserController : AdminController
    {
        public AdminUserController(IUserRepository repos) : base(repos)
        {
            
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}