using gulfsoccer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using WordPressPCL;

namespace gulfsoccer.Areas.Admin.Controllers
{
    public class WordpressController : Controller
    {
        // GET: Admin/Wordpress
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public HttpStatusCode index(int wpPostID)
        {
            PostUtils.setWpServerUri("https://khbar4u.com/wp-json/");
            var post = PostUtils.AddWordpressPost(wpPostID);

            if(post != null)
            {
                Console.Write(Json(post).ToString());
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.Found;
            }
        }

        [HttpPost]
        public async Task<HttpStatusCode> ImportCategories()
        {
            PostUtils.setWpServerUri("https://khbar4u.com/wp-json/");
            await PostUtils.CreateWordPressCategories();

                return HttpStatusCode.OK;
        }

        [HttpPost]
        public async Task<HttpStatusCode> ImportTags()
        {
            PostUtils.setWpServerUri("https://khbar4u.com/wp-json/");
            await PostUtils.CreateWordPressTags();

            return HttpStatusCode.OK;
        }

        public async Task<JsonResult> GetPost()
        {
            
            var client = new WordPressClient("https://khbar4u.com/wp-json/");
            var post = await client.Posts.GetByID(11333);
            return Json(post, JsonRequestBehavior.AllowGet);
        }
    }
}