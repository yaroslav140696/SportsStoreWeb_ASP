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
    
    public class AdminController : Controller
    {
        static string TableName;
        public AdminController()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
        }
        public PartialViewResult TablesName()
        {
            ViewBag.SelectedTable = TableName ?? "Товары";
            List<string> tables = new List<string>() { "Товары", "Пользователи", "Заказы", "Списки желаемого", "Корзины пользователя" };
            return PartialView(tables);
        }

        public RedirectToRouteResult Tables(string tableName = "Товары")
        {
            TableName = tableName;
            string controllerName = null;
            switch (tableName)
            {
                case "Товары":
                    controllerName = "AdminProduct";
                    break;
                case "Пользователи":
                    controllerName = "AdminUser";
                    break;
                default: break;
            }
            return RedirectToAction("Index", controllerName);
        } 
    }
}