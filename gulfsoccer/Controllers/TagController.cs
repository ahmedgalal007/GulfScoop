using DAL.Database;
using gulfsoccer.Models;
using gulfsoccer.Models.gulfsoccer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Controllers
{
    public class TagController : Controller
    {
        private ApplicationDbContext _db;
        public TagController()
        {
            this._db = new ApplicationDbContext();
        }
        public TagController(ApplicationDbContext db)
        {
            this._db = db;
        }
        // GET: Tag
        public ActionResult Index(int Id)
        {
            Tag cat = this._db.Tags.Find(Id);
            List<PostViewModel_Short> result = new List<PostViewModel_Short>();
            if (cat != null)
            {

                IEnumerable<int> IDS = this._db.PostTags.Where(PC => PC.TagId == Id).Select(PS => PS.PostId);
                this._db.Posts.Where(P => IDS.Contains(P.Id)).ToList().ForEach(PVM =>
                {
                    result.Add(PostViewModel_Short.getPostViewModel(PVM, this._db));
                });

            }
            return View("PostGrid", result);
        }
    }
}