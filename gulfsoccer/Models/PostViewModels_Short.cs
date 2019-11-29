using DAL.Database;
using gulfsoccer.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gulfsoccer.Models.gulfsoccer
{
    public class PostViewModel_Short
    {
        public int id { get; set; }
        public string title { get; set; }

        public string body { get; set; }

        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public Category mainCategory { get; set; }

        public Media featuredImage { get; set; }

        public string owner { get; set; }
        public string permalink { get; set; }

        public static PostViewModel_Short getPostViewModel(Post dbPost, ApplicationDbContext db)
        {
            PostViewModel_Short model = new PostViewModel_Short();
            model.id = dbPost.Id;
            model.title = dbPost.Title;
            model.body = TextConverter.StringToParagraphs(dbPost.Body).Count()>0?TextConverter.StringToParagraphs(dbPost.Body).First().Substring(0,156):"";
            model.created = dbPost.Created;
            model.updated = dbPost.Updated;
            model.owner = dbPost.Owner;
            try
            {
                model.featuredImage = dbPost.FeaturedImage > 0 ? db.Medias.Where(I => I.Id == dbPost.FeaturedImage).FirstOrDefault() : new Media();

                var permalink = db.PermaLinks.Where(PL => PL.PostId == dbPost.Id).Select(PL => PL.Link).FirstOrDefault();
                model.permalink = !String.IsNullOrEmpty(permalink) ? "/Post/" + permalink : "/Post/Id/" + dbPost.Id;
            }
            catch (Exception)
            {

            }

            var paragraphes = TextConverter.StringToParagraphs(model.body);
            return model;
        }
    }
}