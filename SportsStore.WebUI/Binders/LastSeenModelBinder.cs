using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Binders
{
    public class LastSeenModelBinder : IModelBinder
    {
        const string sessionkey = "LastSeen";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            LastSeen lastseen = null;
            if (controllerContext.HttpContext.Session != null)
            {
                lastseen = (LastSeen)controllerContext.HttpContext.Session[sessionkey];
            }
            if (lastseen == null)
            {
                lastseen = new LastSeen ();
                lastseen.lastSeen = new List<Product>(5);
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionkey] = lastseen;
                }
            }
            return lastseen;
        }
    }
}