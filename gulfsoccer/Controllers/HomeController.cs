using gulfsoccer.Models;
using gulfsoccer.Models.gulfsoccer;
using gulfsoccer.utilities.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using WordPressPCL;
using WordPressPCL.Models;

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

        public  ActionResult Index()
        {
            // WordPressClient client = Task.Run(() => GetClient()).Result; 

            // var test = GetClient();
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

        #region "WorpressPCL"
        public static string GetClient()
        // private  static WordPressClient GetClient()
        {
            // JWT authentication
            var client =  new WordPressClient("https://khbar4u.com/wp-json/");
            client.AuthMethod = AuthMethod.JWT;
            // var post =  client.Posts.GetByID(11333).Result;
            // client.AuthMethod = AuthMethod.JWT;

            Task.Run(() => client.RequestJWToken("ahmedgalal007", "Sico007_")).ConfigureAwait(true).GetAwaiter().GetResult();
           
                //client.HttpResponsePreProcessing = (res) =>
                //{
                //    var newres = res;
                //    // var clearedString = responseString.Replace("\n", "");
                //    // var regex = @"\<head(.+)body\>";
                //    // return System.Text.RegularExpressions.Regex.Replace(clearedString, regex, "");
                //    // Do something here on the updatedResponse
                //    return newres;
                //};

                if (client.IsValidJWToken().Result)
                {
                    
                    var post = client.Posts.GetByID(11333).Result;
                    //    // var post2 = JsonConvert.DeserializeObject<Post>(post);
                    Console.WriteLine(post.Title.Raw);
                    return post.Title.Raw;
                };

            

            
            return "";
        }



        public async Task<JsonResult> GetPost()
        {
            var client = new WordPressClient("https://khbar4u.com/wp-json/");
            var post = await client.Posts.GetByID(11333);
            return Json(post, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}