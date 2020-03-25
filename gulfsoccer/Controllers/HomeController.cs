using gulfsoccer.Models;
using gulfsoccer.Models.gulfsoccer;
using gulfsoccer.utilities.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace gulfsoccer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController()
        {
            this._db = new ApplicationDbContext();
        }

        public HomeController(ApplicationDbContext Db)
        {
            this._db = Db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mdb()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult getLatestPosts(int count, int skip)
        {
            List<PostViewModel> posts = new List<PostViewModel>();

            this._db.Posts.OrderByDescending(P => P.Created).Take(count).Skip(skip).ToList().ForEach(PA =>
            {
                posts.Add(PostViewModel.getPostViewModel(PA, this._db));
            });

            return Json(JConvert.serializePosts(posts), JsonRequestBehavior.AllowGet);
            // return Json(new { foo = "bar", baz = "Blech" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getLatestCategoryPosts(int count, int skip, int category)
        {
            List<PostViewModel> posts = new List<PostViewModel>();

            this._db.Posts.Where(PST => this._db.PostCategories.Where(PC => PC.CategoryId == category).Select(PCI => PCI.PostId).Contains(PST.Id)).OrderByDescending(PS => PS.Created).Take(count).Skip(skip).ToList().ForEach(PA =>
           {
               posts.Add(PostViewModel.getPostViewModel(PA, this._db));
           });

            return Json(JConvert.serializePosts(posts), JsonRequestBehavior.AllowGet);
            // return Json(new { foo = "bar", baz = "Blech" }, JsonRequestBehavior.AllowGet);
        }

        #region "Helpers"

        //public PostViewModel getPostViewModel(Post dbPost, ApplicationDbContext db)
        //{
        //    PostViewModel model = new PostViewModel();
        //    model.id = dbPost.Id;
        //    model.title = dbPost.Title;
        //    model.body = dbPost.Body;
        //    model.created = dbPost.Created;
        //    model.updated = dbPost.Updated;
        //    model.owner = dbPost.Owner;
        //    try
        //    {
        //        List<Tag> tags = db.Tags.Where(T => db.PostTags.Where(PT => PT.PostId == dbPost.Id).Select(PTID => PTID.TagId).Contains(T.Id)).ToList();
        //        List<Category> categories = db.Categories.Where(C => db.PostCategories.Where(P => P.PostId == dbPost.Id).Select(PC => PC.CategoryId).Contains(C.id)).ToList();
        //        model.categories = categories;
        //        model.featuredImage = dbPost.FeaturedImage > 0 ? db.Medias.Where(I => I.Id == dbPost.FeaturedImage).FirstOrDefault() : new Media();

        //        model.tags = tags;
        //        var permalink = db.PermaLinks.Where(PL => PL.PostId == dbPost.Id).Select(PL => PL.Link).FirstOrDefault();
        //        model.permalink = !String.IsNullOrEmpty(permalink) ? "/Post/" + permalink : "/Post/Id/" + dbPost.Id;
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    return model;
        //}

        #endregion "Helpers"
    }
}