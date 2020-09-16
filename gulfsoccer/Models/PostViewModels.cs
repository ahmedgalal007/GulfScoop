using DAL.Database;
using gulfsoccer.utilities;
using gulfsoccer.utilities.JSON;
using ImageProccessingDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace gulfsoccer.Models.gulfsoccer
{
    public class PostViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public Category mainCategory { get; set; }
        public List<Category> categories { get; set; }
        public List<ThumbSize> thumbSizes { get; set; }
        public List<Tag> tags { get; set; }

        public Media featuredImage { get; set; }
        public string thumbBoxSizes { get; set; }
        public string owner { get; set; }
        public string permalink { get; set; }

        public static PostViewModel getPostViewModel(Post dbPost, ApplicationDbContext db)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            PostViewModel model = new PostViewModel();
            model.id = dbPost.Id;
            model.title = dbPost.Title;
            model.body = dbPost.Body;
            model.created = dbPost.Created;
            model.updated = dbPost.Updated;
            model.owner = dbPost.Owner;
            try
            {
                model.mainCategory = db.Categories.Where(MC => MC.id == dbPost.category).FirstOrDefault();
                List<Tag> tags = db.Tags.Where(T => db.PostTags.Where(PT => PT.PostId == dbPost.Id).Select(PTID => PTID.TagId).Contains(T.Id)).ToList();
                List<Category> categories = db.Categories.Where(C => db.PostCategories.Where(P => P.PostId == dbPost.Id).Select(PC => PC.CategoryId).Contains(C.id)).ToList();
                model.categories = categories;
                model.featuredImage = dbPost.FeaturedImage > 0 ? db.Medias.Where(I => I.Id == dbPost.FeaturedImage).FirstOrDefault() : new Media();

                model.tags = tags;
                var permalink = db.PermaLinks.Where(PL => PL.PostId == dbPost.Id).Select(PL => PL.Link).FirstOrDefault();
                model.permalink = !String.IsNullOrEmpty(permalink) ? "/Post/" + permalink : "/Post/Id/" + dbPost.Id;

                model.thumbSizes = db.ThumbSizes.ToList();
                List<CropBox> CropBoxList = db.Thumbnails.Where(TH => TH.MediaId == dbPost.FeaturedImage).Select(T => new CropBox { 
                     ThumbSizeId = T.ThumbSizeId,
                      x = T.x, y=T.y, width = T.width, height=T.height, boxWidth =T.boxWidth, boxHeight= T.boxHeight,
                       left =T.left, top=T.top, rotate=T.rotate, scaleX=T.scaleX, scaleY=T.scaleY
                }).ToList();
                model.thumbBoxSizes = Json.Encode(CropBoxList);
            }
            catch (Exception)
            {
            }

            var paragraphes = TextConverter.StringToParagraphs(model.body);
            return model;
        }
    }
}