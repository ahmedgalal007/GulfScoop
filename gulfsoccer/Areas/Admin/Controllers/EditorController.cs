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