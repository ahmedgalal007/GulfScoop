using DAL.Database;
using gulfsoccer.Areas.Admin.Models.PostViewModels;
using gulfsoccer.Models;
using gulfsoccer.utilities;
using ImageProccessingDotNet;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using WordPressPCL;
using HtmlAgilityPack;
using System.Net;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace gulfsoccer.Utils
{
    public static class PostUtils
    {
        private static TempApplicationDbContext _db = new TempApplicationDbContext();
        private static WordPressClient _client;

        public static Post AddPost(WordpressPostViewModel post)
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
            if (_db.Medias.Where(I => I.Uri == "/images/" + post.featuredImage).ToList().Count() < 1)
            {
                Media img = new Media { Name = post.title, Localpath = HttpContext.Current.Server.MapPath("/images/" + post.featuredImage), Alt = post.title, Description = post.title, Type = "image", Uri = "/images/" + post.featuredImage };
                _db.Medias.Add(img);
                _db.SaveChanges();
                newPost.FeaturedImage = img.Id;
            }
            else
            {
                newPost.FeaturedImage = _db.Medias.Where(I => I.Uri == "/images/" + post.featuredImage).First().Id;
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
                string ImagePath = _db.Medias.Where(I => I.Uri == ("/images/" + post.featuredImage)).First().Uri;
                string ImageName = Path.GetFileName(HttpContext.Current.Server.MapPath(ImagePath));
                string thumbsize = _db.ThumbSizes.Where(TZ => TZ.Id == item.ThumbSizeId).SingleOrDefault().Name;
                string baseDir = "/Content/Thumbnails/" + thumbsize + "/";
                string outPath = baseDir + ImageName;
                MagicScalerFactory.ProccessImag(HttpContext.Current.Server.MapPath(ImagePath), HttpContext.Current.Server.MapPath(outPath), item);
                i++;
            }
            // _db.Thumbnails.AddRange(ThumbList);
            _db.SaveChanges();
            

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

            _db.SaveChanges();

            // Add Old Post Link
            if (string.IsNullOrWhiteSpace(post.OldPostlink))
            {
                OldPostLink oldLink = new OldPostLink() { PostId = post.id, Link = post.OldPostlink.TrimStart(_client.getServerUrl().ToCharArray()) };
                _db.OldPostLinks.Add(oldLink);
            }

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
            _db.SaveChanges();


            List<string> postCatArr = new List<string>();
            if (!string.IsNullOrEmpty(post.tags))
            {
                postCatArr = post.categories.Split(',').ToList();
            }
            foreach (var catId in postCatArr)
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
                if (!postCatArr.ToList().Contains(C.CategoryId.ToString()))
                {
                    _db.PostCategories.Remove(C);
//                    _db.SaveChanges();
                }
            });

            // Add Tags
            List<string> postTagsArr = new List<string>();
            if (!string.IsNullOrEmpty(post.tags))
            {
                postTagsArr = post.tags.Split(',').ToList();
            }
            foreach (var tagId in postTagsArr)
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
                if (!postTagsArr.ToList().Contains(TT.TagId.ToString()))
                {
                    _db.PostTags.Remove(TT);
                    _db.SaveChanges();
                }
            });

            

           /** return RedirectToAction("Edit", new { id = newPost.Id }); **/


            return newPost;
        }

        public static Post AddWordpressPost(int wpPostID)
        {
            Post newPost;
            // 1- Get the Wordpress Post
            var wpPost = _client.Posts.GetByID(wpPostID).ConfigureAwait(true).GetAwaiter().GetResult();
            // 2- Check the Wordpress Permalink? if exist in the OldPostLink then return, else continue
            var referalLink = UrlConvertToReferal(_client.getServerUrl(), wpPost.Link);
            if (_db.OldPostLinks.Where(OP => OP.Link == referalLink).ToList().Count > 0) return null;

            // 3- Ckeck for categories if not exists Add it to the DB.


            // 4- Ckeck for Tags if not exists Add it to the DB.


            // 5- Create the PostViewModel
            WordpressPostViewModel model = new WordpressPostViewModel() {
                created = wpPost.Date,
                body = wpPost.Content.Rendered,
                categories = string.Join(",", wpPost.Categories),
                description = html2text(wpPost.Excerpt.Rendered),
                featuredImagethumbs = "",
                id = 0,
                mainCategory = wpPost.Categories.FirstOrDefault().ToString(),
                owner = "",
                permalink = wpPost.Link,
                tags = string.Join(",", wpPost.Tags),
                title = wpPost.Title.Rendered,
                updated = DateTime.Now,
                OldPostlink = UrlConvertToReferal(_client.WordPressUri, wpPost.Link),
                OldPostId = wpPost.Id
            };


            // 6- Upload the Mdia Files to the Disk And Add it to the PostViewModel
            var media = getWpPostFeaturedMedia(wpPost.FeaturedMedia.Value);
            SaveImageToDisk(media,ref model);
            model.featuredImage = media;

            // 7- Add the Post to the Database
            newPost = AddPost(model);

            // 6- Save the Wordpress permaLink to the OldPostLink table
            _db.OldPostLinks.Add(new OldPostLink { PostId = newPost.Id, Link = HttpUtility.UrlEncode(referalLink) });
            _db.SaveChanges();

            return newPost;
        }


        public static string getWpPostFeaturedMedia(int MediaIDI)
        {
            WordPressPCL.Models.MediaItem lnk = _client.Media.GetByID(MediaIDI).ConfigureAwait(true).GetAwaiter().GetResult();
            return  lnk.MediaDetails.File;
        }

        public static string UrlConvertToReferal(string serverUrl, string link)
        {
            var uri = new Uri(serverUrl);

            if (link.StartsWith(_client.getServerUrl()))
            {
                link = link.TrimStart(_client.getServerUrl().ToCharArray());
            } else if(link.StartsWith(_client.getServerUrl(false)))
            {
                link = link.TrimStart(_client.getServerUrl(false).ToCharArray());
            }

            if (! link.StartsWith("/"))
            {
                link = "/" + link;
            }

            return link;
        }


        public static async Task<int> CreateWordPressCategories()
        {
            await _db.Database.ExecuteSqlCommandAsync(@"SET IDENTITY_INSERT [dbo].[Categories] ON");
            // var client = new WordPressClient(SiteUri);
            var categories = await _client.Categories.GetAll();
            foreach (var cat in categories.ToList().OrderBy(c => c.Id))
            {
                var UpdateID = cat.Id;
                if( _db.Categories.Find(cat.Id) == null)
                {
                    _db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Categories] ON; INSERT INTO [dbo].[Categories] (id,name,ParentId,InMenu ) VALUES (" + cat.Id +", N'" + cat.Name + "', null, 0); SET IDENTITY_INSERT [dbo].[Categories] OFF;");
                    //_db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Categories] ON");

                    //_db.SaveChanges();
                    //var newCat = new Category { name = cat.Name, ParentId= null, InMenu = false };
                    //if(cat.Parent > 0 ) newCat.ParentId = cat.Parent ;
                    //_db.Categories.Add(newCat);
                    //_db.SaveChanges();
                    //_db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Categories] OFF");
                    //_db.SaveChanges();

                    // UpdateID = newCat.id;
                } else
                {

                    _db.Database.ExecuteSqlCommand(@"UPDATE [dbo].[Categories] SET name = N'"  + cat.Name + "' WHERE id =" + cat.Id + ";");

                }

                //var updatedCat = _db.Categories.Find(UpdateID);
                //updatedCat.id = cat.Id;
                //updatedCat.name = cat.Name;
                //if(cat.Parent > 0 )
                //    updatedCat.ParentId = cat.Parent;
                //_db.Categories.Attach(updatedCat);
                //_db.Entry(updatedCat).State = System.Data.Entity.EntityState.Modified;
                //_db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Categories] ON");
                //_db.SaveChanges();
                //_db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Categories] OFF");


            }

            foreach (var cat in categories.ToList().OrderBy(c => c.Id))
            {
                if(cat.Parent > 0)
                    _db.Database.ExecuteSqlCommand(@"UPDATE [dbo].[Categories] SET ParentId = " + cat.Parent + " WHERE id =" + cat.Id + ";");
            }

            return 1;
        }

        public static async Task<int> CreateWordPressTags()
        {
            // var client = new WordPressClient(SiteUri);
            var tags = await _client.Tags.GetAll();
            foreach (var tag in tags.ToList().OrderBy(t => t.Id))
            {
                if (_db.Tags.Find(tag.Id) == null)
                {
                    //_db.Tags.Add(new  DAL.Database.Tag{ Name = tag.Name, Id = tag.Id });
                    _db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Tags] ON; 
                                                    INSERT INTO [dbo].[Tags] (Id,Name ) VALUES (" + tag.Id + ", N'" + tag.Name + "'); " +
                                                    "SET IDENTITY_INSERT [dbo].[Tags] OFF;");
                }
                else
                {
                    _db.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Tags] ON; 
                                                    UPDATE [dbo].[Tags] SET Name = N'" + tag.Name + "' WHERE Id = " + tag.Id + "; " +
                                                    "SET IDENTITY_INSERT [dbo].[Tags] OFF;");

                    //var updatedTag = _db.Tags.Find(tag.Id);
                    //updatedTag.Name = tag.Name;
                    //_db.Tags.Attach(updatedTag);
                    //_db.Entry(updatedTag).State = System.Data.Entity.EntityState.Modified;
                }
            }
            _db.SaveChanges();
            return 1;
        }
        #region "POST"
        public static void setWpServerUri(string ServerURL)
        {
            _client = new WordPressClient(ServerURL);
        }


        public static string getServerUrl(this WordPressClient cl, bool WWW = true)
        {
            var uri = new Uri(cl.WordPressUri);
            if (WWW)
            {
                return uri.Scheme + "://www." + uri.Host;
            }
            return uri.Scheme + "://" + uri.Host;
        }

        public static string html2text(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(@"<html><body>" + html + "</body></html>");
            return doc.DocumentNode.SelectSingleNode("//body").InnerText;
        }

        public static void SaveImageToDisk( string imgPath, ref WordpressPostViewModel VM)
        {
            var root = HttpContext.Current.Server.MapPath("/images");
            var pathArr = imgPath.TrimStart('/').TrimEnd('/').Split('/');

            var tmp = root;
            for (int i = 0; i < pathArr.Length -1 ; i++)
            {
                if (!Directory.Exists(tmp + "\\" + pathArr[i]))
                {
                    Directory.CreateDirectory(tmp + "\\" + pathArr[i]);
                }
                tmp = tmp + "\\" + pathArr[i];
            }
            using (var client = new WebClient())
            {
                string src = _client.getServerUrl().TrimEnd('/') + "/wp-content/uploads/" + imgPath;
                string dest = tmp + "\\" + pathArr[pathArr.Length - 1];
                client.DownloadFile(src , dest);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                VM.featuredImagethumbs = serializer.Serialize( calculateDefaultThumbs(dest));
            }
        }


        public static List<CropBox> calculateDefaultThumbs(string file)
        {
            List<CropBox> thumbs = new List<CropBox>();
            _db.ThumbSizes.ToList().ForEach(thubSize =>
            {
                thumbs.Add(CreateThumbnailsFromRatio(file, Id: thubSize.Id, rX: thubSize.Width, rY: thubSize.Height));
            });
            //List<Tuple<int, int, int>> ratio = new List<Tuple<int,int, int>>();
            //ratio.Add(new Tuple<int, int, int>(1,  4,  3 ));
            //ratio.Add(new Tuple<int, int, int>(3, 16, 9));
            //ratio.Add(new Tuple<int, int, int>(4, 1, 1));

            //foreach (var item in ratio)
            //{
            //    thumbs.Add(CreateThumbnailsFromRatio(file, Id:item.Item1 , rX: item.Item2, rY: item.Item3));
            //}

            return thumbs;
        }

        private static CropBox CreateThumbnailsFromRatio(string file, int Id, int rX, int rY)
        {
            CropBox box = new CropBox() {
                scaleX = 1,
                scaleY = 1, 
                rotate=0, 
                ThumbSizeId = Id
            };

            System.Drawing.Image img = System.Drawing.Image.FromFile(file);
            float srcRatio = img.Width / img.Height;
            float thumbRatio = rX / rY;
            if(srcRatio > thumbRatio)
            {
                //calculate according to Height
                box.height = img.Height;
                box.width = img.Height * rX / rY;
            }
            else
            {
                //claculate according to Width
                box.width = img.Width;
                box.height = img.Width * rY / rX;
                box.x = 0;
            }
            box.left = (img.Width - box.width) > 0 ? (img.Width - box.width) / 2 : 0;
            box.x = box.left;
            box.top = (img.Height - box.height) > 0 ? (img.Height - box.height) / 2 : 0;
            box.y = box.top;
            box.boxWidth = box.width;
            box.boxHeight = box.height;

            return box;
        }
        #endregion
    }


    public class WordpressPostViewModel: CreatePostViewModel
    {
        public string OldPostlink { get; set; }
        public int OldPostId { get; set; }
    }


    public class TempApplicationDbContext : ApplicationDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
              .Property(a => a.id)
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            base.OnModelCreating(modelBuilder);
        }
    }


}
