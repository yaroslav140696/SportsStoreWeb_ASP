using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers.Abstract
{
    public interface IAdminController<T>
    {
        ActionResult Index();
        ViewResult Edit(int T_id);
        [HttpPost]
        ActionResult Edit(T item);
        ViewResult Create();
        [HttpPost]
        ActionResult Delete();
    }
}
