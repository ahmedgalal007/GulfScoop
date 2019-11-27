using gulfsoccer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Areas.Admin.Controllers
{
    public class CategoryTreeViewController : Controller
    {

        public ActionResult Remote_Data_Binding()
        {
            return View();
        }

        public JsonResult Remote_Data_Binding_Get_Categories(int? id)
        {
            var dataContext = new ApplicationDbContext();

            var categories = from e in dataContext.Categories
                            where id.HasValue ? e.ParentId == id : e.ParentId == null 
                            select new
                            {
                                id = e.id,
                                Name = e.name,
                                hasChildren = e.Children.Any()
                            };

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

    }
}