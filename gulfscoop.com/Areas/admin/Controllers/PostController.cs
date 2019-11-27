using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfscoop.com.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin, Author, Editor")]
    [RouteArea("admin")]
    [RoutePrefix("")]
    [Route("{action}")]
    public class PostController : Controller
    {
        // GET: admin/Post
        public ActionResult Index()
        {
            return View();
        }
    }
}