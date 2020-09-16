using DAL.Database;
using gulfsoccer.Areas.Admin.Models.PostViewModels;
using gulfsoccer.Models;
using gulfsoccer.Models.gulfsoccer;
using gulfsoccer.utilities;
using gulfsoccer.utilities.JSON;
using ImageProccessingDotNet;
using Kendo.Mvc.Extensions;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Telerik.Windows.Documents.Fixed.Model.Editing.Lists;

namespace gulfsoccer.Areas.Admin.Controllers
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

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int id)
        {
            Post post = this._db.Posts.Find(id);
            return View();
        }

        // GET: Admin/Post/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Post/Create
        //public ActionResult Create(FormCollection collection)
        [HttpPost]
        [Authorize]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel post)
        {
            Post newPost;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            // use the JavaScriptSerializer to deserialize our json into the expected object
            List<CropBox> Thumbs = serializer.Deserialize<List<CropBox>>(post.featuredImagethumbs);
            if (post.id > 0)
            {
                newPost = this._db.Posts.Find(post.id);
                newPost.Body = post.body;
                newPost.Created = post.created;
                newPost.FeaturedAlbum = 0;
                if (this._db.Categories.Where(I => I.name == post.mainCategory).Count() > 0 && !string.IsNullOrEmpty(post.mainCategory))
                {
                    newPost.category = this._db.Categories.Where(I => I.name == post.mainCategory).FirstOrDefault().id;
                }
                newPost.Owner = post.owner;
                newPost.Title = post.title;
                newPost.Updated = post.updated;
            }
            else
            {
                newPost = new Post() { Id = post.id, Title = post.title, Created = post.created, Updated = post.updated, Body = post.body, Owner = post.owner };
            }

            string[] err = new string[] { };
            //try
            //{
            // TODO: Add insert logic here
            //UriBuilder urlBuilder = new UriBuilder(Request.Url.AbsoluteUri){
            //                                Path = Url.Content(post.featuredImage),
            //                                Query = null,
            //                            };
            //Uri uri = urlBuilder.Uri;
            //string url = urlBuilder.ToString();

            // First Add and Save the Featured Image
            //try
            //{
            //  newPost.FeaturedImage = post.featuredImage
            if (String.IsNullOrEmpty(post.description))
            {
                newPost.Description = TextConverter.StringToParagraphs(post.body).Count() > 0 ? TextConverter.StringToParagraphs(post.body).First().Substring(0, 156) : "";
            }
            else
            {
                newPost.Description = post.description;
            }
            if (this._db.Medias.Where(I => I.Uri == post.featuredImage).ToList().Count() < 1)
            {
                Media img = new Media { Name = post.title, Localpath = HttpContext.Server.MapPath(post.featuredImage), Alt = post.title, Description = post.title, Type = "image", Uri = post.featuredImage/*HttpContext.Server.MapPath(post.featuredImage) Path.GetFullPath(post.featuredImage)*/ };
                this._db.Medias.Add(img);
                this._db.SaveChanges();
                newPost.FeaturedImage = img.Id;
            }
            else
            {
                newPost.FeaturedImage = this._db.Medias.Where(I => I.Uri == post.featuredImage).First().Id;
            }

            ///  Add Thumbnails
            int i = 0;
            // List<Thumbnails> ThumbList = _db.Thumbnails.Where(TH => TH.MediaId == newPost.FeaturedImage).ToList();
            foreach (CropBox item in Thumbs)
            {
                List<Thumbnails> ThumbList = _db.Thumbnails.Where(TH => TH.MediaId == newPost.FeaturedImage && TH.ThumbSizeId == item.ThumbSizeId).ToList();
                if (ThumbList.Count() == 1 )
                {
                    ThumbList[0].MediaId = newPost.FeaturedImage;
                    ThumbList[0].x = item.x;
                    ThumbList[0].y = item.y;
                    ThumbList[0].width = item.width;
                    ThumbList[0].height = item.height;
                    ThumbList[0].left = item.left;
                    ThumbList[0].top = item.top;
                    ThumbList[0].boxWidth = item.boxWidth;
                    ThumbList[0].boxHeight = item.boxHeight;
                    ThumbList[0].rotate = item.rotate;
                    ThumbList[0].scaleX = item.scaleX;
                    ThumbList[0].scaleY = item.scaleY;
                    ThumbList[0].ThumbSizeId = item.ThumbSizeId;
                    this._db.Thumbnails.Attach(ThumbList[0]);
                    this._db.Entry(ThumbList[0]).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    _db.Thumbnails.Add(new Thumbnails
                    {
                        MediaId = newPost.FeaturedImage,
                        x = item.x,
                        y = item.y,
                        width = item.width,
                        height = item.height,
                        left = item.left,
                        top = item.top,
                        boxWidth = item.boxWidth,
                        boxHeight = item.boxHeight,
                        rotate = item.rotate,
                        scaleX = item.scaleX,
                        scaleY = item.scaleY,
                        ThumbSizeId = item.ThumbSizeId
                    });
                }

                // Create The Thumbnail on the Disk
                string ImagePath = this._db.Medias.Where(I => I.Uri == post.featuredImage).First().Uri;
                string ImageName = Path.GetFileName(Server.MapPath(ImagePath));
                string thumbsize = _db.ThumbSizes.Where(TZ => TZ.Id == item.ThumbSizeId).SingleOrDefault().Name;
                string baseDir = "/Content/Thumbnails/" + thumbsize + "/";
                string outPath = baseDir + ImageName;
                MagicScalerFactory.ProccessImag(Server.MapPath(ImagePath), Server.MapPath(outPath), item);
                i++;
            }
            // _db.Thumbnails.AddRange(ThumbList);
            _db.SaveChanges();
            //}
            //catch (Exception e)
            //{
            //    err[err.Length]= e.Message;
            //    return View(post);
            //}
            //finally
            //{
            if (newPost.Id > 0)
            {
                this._db.Posts.Attach(newPost);
                this._db.Entry(newPost).State = System.Data.Entity.EntityState.Modified;
                // this._db.Entry(newPost).Property("FeaturedImage").IsModified = true;
            }
            else
            {
                this._db.Posts.Add(newPost);
            }

            this._db.SaveChanges();
            //}

            //}
            //catch(Exception e)
            //{
            //    err[err.Length] = e.Message;
            //    return View(post);

            //}
            //finally
            //{
            //try
            //{
            int categoryID, tagID = 0;
            if (_db.PermaLinks.Where(PL => PL.PostId == newPost.Id).ToList().Count > 0)
            {
                PermaLink permalink = _db.PermaLinks.Where(PL => PL.PostId == newPost.Id).FirstOrDefault();
                _db.PermaLinks.Attach(permalink);
                this._db.Entry(permalink).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                _db.PermaLinks.Add(new PermaLink { PostId = newPost.Id, Link = Post.Slugify(post.title) });
            }
            this._db.SaveChanges();

            foreach (var catId in post.categories.Split(','))
            {
                categoryID = int.Parse(catId);
                if (_db.PostCategories.Where(PC => PC.PostId == newPost.Id && PC.CategoryId == categoryID).Count() == 0)
                {
                    _db.PostCategories.Add(new PostCategory { PostId = newPost.Id, CategoryId = categoryID });
                    _db.SaveChanges();
                }
            }
            _db.PostCategories.Where(PC => PC.PostId == newPost.Id).ToList().ForEach(C =>
            {
                if (!post.categories.Split(',').ToList().Contains(C.CategoryId.ToString()))
                {
                    _db.PostCategories.Remove(C);
                    _db.SaveChanges();
                }
            });

            foreach (var tagId in post.tags.Split(','))
            {
                tagID = int.Parse(tagId);
                if (_db.PostTags.Where(PT => PT.PostId == newPost.Id && PT.TagId == tagID).Count() == 0)
                {
                    _db.PostTags.Add(new PostTag { PostId = newPost.Id, TagId = tagID });
                    _db.SaveChanges();
                }
            }

            _db.PostTags.Where(PT => PT.PostId == newPost.Id).ToList().ForEach(TT =>
            {
                if (!post.tags.Split(',').ToList().Contains(TT.TagId.ToString()))
                {
                    _db.PostTags.Remove(TT);
                    _db.SaveChanges();
                }
            });

            //}
            //catch(Exception e)
            //{
            //    err[err.Length] = e.Message;
            //}

            //}

            return RedirectToAction("Edit", new { id = newPost.Id });
        }

        // GET: Admin/Post/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            PostViewModel model = PostViewModel.getPostViewModel(this._db.Posts.Find(id), this._db);
            return View(model);
        }

        // POST: Admin/Post/Edit/5
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Post/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}