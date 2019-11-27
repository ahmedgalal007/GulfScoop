using gulfsoccer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace gulfsoccer.Areas.Admin.Controllers
{
    public class AutocompeleteController : Controller
    {
        private readonly ApplicationDbContext  _db = new ApplicationDbContext();

        public ActionResult ServerFiltering()
        {
            return View();
        }
        // GET: Admin/Autocompelete
        public JsonResult FilterUsersList([FromUri]string filter)
        {

            // var options = new List<KendoSelectOption>() { new KendoSelectOption { ID = 1, Name = "one" }, new KendoSelectOption { ID = 2, Name = "Two" }, new KendoSelectOption { ID = 3, Name = "Three" } };
           var options = new List<KendoSelectOption>();

            if (!string.IsNullOrEmpty(filter))
            {

                options = _db.Users.Select(U => new KendoSelectOption { ID = U.UserName, Name = U.UserName }).Where(p => p.Name.Contains(filter)).ToList();
            }

            return Json(options, JsonRequestBehavior.AllowGet);
        }
        class Filter
        {
            public int MyProperty { get; set; }
        }
    }
   
}