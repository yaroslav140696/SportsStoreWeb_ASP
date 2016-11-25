using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Binders
{
    public class UserModelBinder : IModelBinder
    {
        const string sessionkey = "CurrentUser"; 
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CurrentUser user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (CurrentUser)controllerContext.HttpContext.Session[sessionkey];
            }
            if (user == null)
            {
                user = new CurrentUser();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionkey] = user;
                }
            }
            return user;
        }
    }
}