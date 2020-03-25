using DAL.Database;
using gulfsoccer.Models;
using gulfsoccer.Models.gulfsoccer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace gulfsoccer.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;

        public CategoryController()
        {
            this._db = new ApplicationDbContext();
        }

        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }

        // GET: Category/Id
        public ActionResult Index(int Id)
        {
            Category cat = this._db.Categories.Find(Id);
            List<PostViewModel_Short> result = new List<PostViewModel_Short>();
            if (cat != null)
            {
                IEnumerable<int> IDS = this._db.PostCategories.Where(PC => PC.CategoryId == Id).Select(PS => PS.PostId);
                this._db.Posts.Where(P => IDS.Contains(P.Id)).ToList().ForEach(PVM =>
                {
                    result.Add(PostViewModel_Short.getPostViewModel(PVM, this._db));
                });
            }
            return View("PostGrid", result);
        }
    }
}