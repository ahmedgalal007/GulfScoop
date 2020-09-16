using DAL.Database;
using gulfsoccer.Areas.Admin.Models.PostViewModels;
using gulfsoccer.Models;
using gulfsoccer.utilities;
using ImageProccessingDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using WordPressPCL;

namespace gulfsoccer.Utils
{
    public static class PostUtils
    {
        private static ApplicationDbContext _db = new ApplicationDbContext();
        private static WordPressClient _client;

        public static Post AddPost(CreatePostViewModel post)
        {

            Post newPost;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            // use the JavaScriptSerializer to deserialize our json into the expected object
            List<CropBox> Thumbs = serializer.Deserialize<List<CropBox>>(post.featuredImagethumbs);
            if (post.id > 0 && _db.Posts.Find(post.id) != null)
            {
                newPost = _db.Posts.Find(post.id);
                newPost.Body = post.body;
                newPost.Created = post.created;
                newPost.FeaturedAlbum = 0;
                if (_db.Categories.Where(I => I.name == post.mainCategory).Count() > 0 && !string.IsNullOrEmpty(post.mainCategory))
                {
                    newPost.category = _db.Categories.Where(I => I.name == post.mainCategory).FirstOrDefault().id;
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
            

            if (String.IsNullOrEmpty(post.description))
            {
                newPost.Description = TextConverter.StringToParagraphs(post.body).Count() > 0 ? TextConverter.StringToParagraphs(post.body).First().Substring(0, 156) : "";
            }
            else
            {
                newPost.Description = post.description;
            }
            if (_db.Medias.Where(I => I.Uri == post.featuredImage).ToList().Count() < 1)
            {
                Media img = new Media { Name = post.title, Localpath = HttpContext.Current.Server.MapPath(post.featuredImage), Alt = post.title, Description = post.title, Type = "image", Uri = post.featuredImage/*HttpContext.Server.MapPath(post.featuredImage) Path.GetFullPath(post.featuredImage)*/ };
                _db.Medias.Add(img);
                _db.SaveChanges();
                newPost.FeaturedImage = img.Id;
            }
            else
            {
                newPost.FeaturedImage = _db.Medias.Where(I => I.Uri == post.featuredImage).First().Id;
            }

            ///  Add Thumbnails
            int i = 0;
            // List<Thumbnails> ThumbList = _db.Thumbnails.Where(TH => TH.MediaId == newPost.FeaturedImage).ToList();
            foreach (CropBox item in Thumbs)
            {
                List<Thumbnails> ThumbList = _db.Thumbnails.Where(TH => TH.MediaId == newPost.FeaturedImage && TH.ThumbSizeId == item.ThumbSizeId).ToList();
                if (ThumbList.Count() == 1)
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
                    _db.Thumbnails.Attach(ThumbList[0]);
                    _db.Entry(ThumbList[0]).State = System.Data.Entity.EntityState.Modified;
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
                string ImagePath = _db.Medias.Where(I => I.Uri == post.featuredImage).First().Uri;
                string ImageName = Path.GetFileName(HttpContext.Current.Server.MapPath(ImagePath));
                string thumbsize = _db.ThumbSizes.Where(TZ => TZ.Id == item.ThumbSizeId).SingleOrDefault().Name;
                string baseDir = "/Content/Thumbnails/" + thumbsize + "/";
                string outPath = baseDir + ImageName;
                MagicScalerFactory.ProccessImag(HttpContext.Current.Server.MapPath(ImagePath), HttpContext.Current.Server.MapPath(outPath), item);
                i++;
            }
            // _db.Thumbnails.AddRange(ThumbList);
//            _db.SaveChanges();
            

            if (newPost.Id > 0)
            {
                _db.Posts.Attach(newPost);
                _db.Entry(newPost).State = System.Data.Entity.EntityState.Modified;
                // this._db.Entry(newPost).Property("FeaturedImage").IsModified = true;
            }
            else
            {
                _db.Posts.Add(newPost);
            }

//            _db.SaveChanges();
            
            // Add Categories 
            int categoryID, tagID = 0;
            if (_db.PermaLinks.Where(PL => PL.PostId == newPost.Id).ToList().Count > 0)
            {
                PermaLink permalink = _db.PermaLinks.Where(PL => PL.PostId == newPost.Id).FirstOrDefault();
                _db.PermaLinks.Attach(permalink);
                _db.Entry(permalink).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                _db.PermaLinks.Add(new PermaLink { PostId = newPost.Id, Link = Post.Slugify(post.title) });
            }
//            _db.SaveChanges();

            foreach (var catId in post.categories.Split(','))
            {
                categoryID = int.Parse(catId);
                if (_db.PostCategories.Where(PC => PC.PostId == newPost.Id && PC.CategoryId == categoryID).Count() == 0)
                {
                    _db.PostCategories.Add(new PostCategory { PostId = newPost.Id, CategoryId = categoryID });
//                    _db.SaveChanges();
                }
            }
            _db.PostCategories.Where(PC => PC.PostId == newPost.Id).ToList().ForEach(C =>
            {
                if (!post.categories.Split(',').ToList().Contains(C.CategoryId.ToString()))
                {
                    _db.PostCategories.Remove(C);
//                    _db.SaveChanges();
                }
            });

            // Add Tags
            foreach (var tagId in post.tags.Split(','))
            {
                tagID = int.Parse(tagId);
                if (_db.PostTags.Where(PT => PT.PostId == newPost.Id && PT.TagId == tagID).Count() == 0)
                {
                    _db.PostTags.Add(new PostTag { PostId = newPost.Id, TagId = tagID });
//                    _db.SaveChanges();
                }
            }

            _db.PostTags.Where(PT => PT.PostId == newPost.Id).ToList().ForEach(TT =>
            {
                if (!post.tags.Split(',').ToList().Contains(TT.TagId.ToString()))
                {
                    _db.PostTags.Remove(TT);
//                    _db.SaveChanges();
                }
            });

            

           /** return RedirectToAction("Edit", new { id = newPost.Id }); **/


            return newPost;
        }

        public static Post AddWordpressPost(int wpPostID)
        {
            Post newPost;
            // 1- Get the Wordpress Post
            var wpPost = _client.Posts.GetByID(11333).ConfigureAwait(true).GetAwaiter().GetResult();
            // 2- Check the Wordpress Permalink? if exist in the OldPostLink then return, else continue
            if (_db.OldPostLinks.Where(OP => OP.Link == wpPost.Link).ToList().Count > 0) return null;

            // 3- Ckeck for categories if not exists Add it to the DB.


            // 4- Ckeck for Tags if not exists Add it to the DB.


            // 5- Create the PostViewModel
            CreatePostViewModel model = new CreatePostViewModel() {
                created = wpPost.Date,
                body = wpPost.Content.Raw,
                categories = string.Join(",", wpPost.Categories),
                description = wpPost.Excerpt.Rendered,
                featuredImage = getWpPostFeaturedMedia(wpPost.FeaturedMedia.Value),
                featuredImagethumbs = "",
                id = wpPost.Id,
                mainCategory = wpPost.Categories.FirstOrDefault().ToString(),
                owner = "",
                permalink = wpPost.Link,
                tags = string.Join("," , wpPost.Tags),
                title = wpPost.Title.Rendered,
                updated = DateTime.Now
            };

            // 6- Upload the Mdia Files to the Disk And Add it to the PostViewModel

            // 7- Add the Post to the Database
            newPost = AddPost(model);

            // 6- Save the Wordpress permaLink to the OldPostLink table
            return newPost;
        }


        public static string getWpPostFeaturedMedia(int MediaIDI)
        {
            return _client.Media.GetByID(MediaIDI).ConfigureAwait(true).GetAwaiter().GetResult().Link;
        }


        public static async Task<int> CreateWordPressCategories(string SiteUri)
        {
            var client = new WordPressClient(SiteUri);
            var categories = await client.Categories.GetAll();
            foreach (var cat in categories.ToList().OrderBy(c => c.Id))
            {
                if( _db.Categories.Find(cat.Id) == null)
                {
                    _db.Categories.Add(new Category { id = cat.Id, name = cat.Name, ParentId = cat.Parent, InMenu = false });
                }
                else
                {
                    var updatedCat = _db.Categories.Find(cat.Id);
                    updatedCat.name = cat.Name;
                    updatedCat.ParentId = cat.Parent;
                    _db.Categories.Attach(updatedCat);
                    _db.Entry(updatedCat).State = System.Data.Entity.EntityState.Modified;
                }
            }
            _db.SaveChanges();
            return 1;
        }

        public static async Task<int> CreateWordPressTags(string SiteUri)
        {
            var client = new WordPressClient(SiteUri);
            var tags = await client.Tags.GetAll();
            foreach (var tag in tags.ToList().OrderBy(c => c.Id))
            {
                if (_db.Categories.Find(tag.Id) == null)
                {
                    _db.Tags.Add(new  DAL.Database.Tag{ Name = tag.Name, Id = tag.Id });
                }
                else
                {
                    var updatedTag = _db.Tags.Find(tag.Id);
                    updatedTag.Name = tag.Name;
                    _db.Tags.Attach(updatedTag);
                    _db.Entry(updatedTag).State = System.Data.Entity.EntityState.Modified;
                }
            }
            _db.SaveChanges();
            return 1;
        }

        public static void setWpServerUri(string ServerURL)
        {
            _client = new WordPressClient(ServerURL);
        }

    }
}