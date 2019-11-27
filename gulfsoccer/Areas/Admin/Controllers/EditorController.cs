using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Areas.Admin.Controllers
{
    public class EditorController : Controller
    {
        // GET: Admin/Editor
        public ActionResult ImageBrowser()
        {
            return View();
        }
    }
}