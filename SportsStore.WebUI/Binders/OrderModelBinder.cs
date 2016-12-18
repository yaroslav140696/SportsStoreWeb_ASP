using SportsStore.Domain.Entities;
using System.Web.Mvc;

namespace SportsStore.WebUI.Binders
{
    public class OrderModelBinder : IModelBinder
    {
        const string sessionkey = "CurrentOrder";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            OrderMainPart CurrentOrder = null;
            if (controllerContext.HttpContext.Session != null)
            {
                CurrentOrder = (OrderMainPart)controllerContext.HttpContext.Session[sessionkey];
            }
            if (CurrentOrder == null)
            {
                CurrentOrder = new OrderMainPart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionkey] = CurrentOrder;
                }
            }
            return CurrentOrder;
        }
    }
}