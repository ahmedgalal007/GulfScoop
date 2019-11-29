using DAL.Database;
using gulfsoccer.Models;
using gulfsoccer.Models.gulfsoccer;
using gulfsoccer.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PostController()
        {
            this._db = new ApplicationDbContext();
        }
        public PostController(ApplicationDbContext Db)
        {
            this._db = Db;
        }
        // GET: Post/Id/1
        public ActionResult Id(int id)
        {
            Post post = _db.Posts.Find(id);
            return View("Post", getPostViewModel(post, _db));
        }
        // GET: Post/PermaLink
        public ActionResult Index(string permaLink)
        {
            Post post = _db.Posts.Find(_db.PermaLinks.Where(L => L.Link==permaLink).FirstOrDefault().PostId);
            return View("Post", getPostViewModel(post, _db));
        }
        

        public PostViewModel getPostViewModel(Post dbPost, ApplicationDbContext db )
        {
            PostViewModel model = new PostViewModel();
            model.id = dbPost.Id;
            model.title = dbPost.Title;
            model.body = dbPost.Body;
            model.description = dbPost.Description;
            model.created = dbPost.Created;
            model.updated = dbPost.Updated;
            model.owner = dbPost.Owner;
            try
            {
                List<Tag> tags = db.Tags.Where(T => db.PostTags.Where(PT => PT.PostId == dbPost.Id).Select(PTID => PTID.TagId).Contains(T.Id)).ToList();
                List<Category> categories = db.Categories.Where(C => db.PostCategories.Where(P => P.PostId == dbPost.Id).Select(PC => PC.CategoryId).Contains(C.id)).ToList();
                model.categories = categories;
                model.featuredImage = dbPost.FeaturedImage > 0 ? db.Medias.Where(I => I.Id == dbPost.FeaturedImage).FirstOrDefault() : new Media();

                model.tags = tags;
                var permalink = db.PermaLinks.Where(PL => PL.PostId == dbPost.Id).Select(PL => PL.Link).FirstOrDefault();
                model.permalink = !String.IsNullOrEmpty(permalink)? "/Post/" + permalink : "/Post/Id/" + dbPost.Id;
            }
            catch (Exception)
            {

            }

            var paragraphes = TextConverter.StringToParagraphs(model.body);
            return model;
        }
    }
}