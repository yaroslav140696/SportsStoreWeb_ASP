using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Binders;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(CurrentUser), new UserModelBinder());
            ModelBinders.Binders.Add(typeof(OrderMainPart), new OrderModelBinder());
        }
        
        public void Session_End()
        {
            ICartRepository repos = new EFShoppingCartRepository();
            repos.EndSession();
            System.Diagnostics.Debug.WriteLine("Session End");
        }
        
    }
}
