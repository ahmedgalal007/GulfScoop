using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfscoop.com.Areas.admin.Controllers
{
    [Authorize]
    [RouteArea("admin")]
    [RoutePrefix("")]
    [Route("{action}")]
    public class HomeController : Controller
    {
        // GET: admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}