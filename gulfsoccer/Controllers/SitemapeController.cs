using DAL.Database;
using gulfsoccer.Models;
using System.Linq;
using System.Web.Mvc;
using System.Xml;

namespace gulfsoccer.Controllers
{
    public class SitemapeController : Controller
    {
        private ApplicationDbContext _db;
        private XmlDocument xmlDocument;

        public SitemapeController()
        {
            this._db = new ApplicationDbContext();
            this.xmlDocument = new XmlDocument();
        }

        public SitemapeController(ApplicationDbContext db)
        {
            this._db = db;
            this.xmlDocument = new XmlDocument();
        }

        // GET: Sitemap
        public ActionResult Index()
        {
            XmlElement siteMape = this.xmlDocument.CreateElement("siteMapes");
            //////////
            XmlElement posts = xmlDocument.CreateElement("siteMape");

            posts.SetAttribute("key", "Posts");
            posts.SetAttribute("url", "/sitemape_posts.xml");
            posts.SetAttribute("title", "Posts");

            siteMape.AppendChild(posts);

            //////////
            XmlElement categories = xmlDocument.CreateElement("siteMape");

            categories.SetAttribute("key", "Categories");
            categories.SetAttribute("url", "/sitemape_categories.xml");
            categories.SetAttribute("title", "Categories");

            siteMape.AppendChild(categories);

            //////////
            XmlElement tags = xmlDocument.CreateElement("siteMape");

            tags.SetAttribute("key", "Tags");
            tags.SetAttribute("url", "/sitemape_tags.xml");
            tags.SetAttribute("title", "Tags");

            siteMape.AppendChild(tags);

            //////////
            XmlElement media = xmlDocument.CreateElement("siteMape");

            media.SetAttribute("key", "Medias");
            media.SetAttribute("url", "/sitemape_medias.xml");
            media.SetAttribute("title", "Medias");

            siteMape.AppendChild(media);

            this.xmlDocument.AppendChild(siteMape);

            this.xmlDocument.Save(Request.MapPath("/") + "sitemape.xml");
            return Content(this.xmlDocument.InnerXml);
        }

        public ActionResult Posts()
        {
            int postsCount = this._db.Posts.Select(P => P.Id).Count();
            //int pages = (int)Math.Ceiling((double)(postsCount / 1000));
            XmlElement mainPostsSiteMape = this.xmlDocument.CreateElement("siteMape");
            for (int i = 0; i < postsCount; i += 1000)
            {
                XmlDocument XDOC = new XmlDocument();
                // Create Pages
                XmlElement XDOCsiteMape = XDOC.CreateElement("siteMape");
                this._db.Posts.OrderBy(pst => pst.Created).Skip(i).Take(i + 1000).ToList<Post>().ForEach(P =>
                {
                    string permalink = this._db.PermaLinks.Find(P.Id).Link;
                    XmlElement post = XDOC.CreateElement("siteMapNode");

                    post.SetAttribute("key", "Post");
                    post.SetAttribute("url", "/Post/" + permalink);
                    post.SetAttribute("title", P.Title);
                    XDOCsiteMape.AppendChild(post);
                });
                XDOC.AppendChild(XDOCsiteMape);
                XDOC.Save(Request.MapPath("/") + "sitemape_posts_P" + (i + 1) + ".xml");
                // Create Main Posts Pages SiteMape

                XmlElement postPage = xmlDocument.CreateElement("siteMapNode");

                postPage.SetAttribute("key", "siteMape");
                postPage.SetAttribute("url", "/" + "sitemape_posts_p" + (i + 1) + ".xml");
                postPage.SetAttribute("title", "sitemape posts page " + (i + 1));
                mainPostsSiteMape.AppendChild(postPage);
            }
            this.xmlDocument.AppendChild(mainPostsSiteMape);
            this.xmlDocument.Save(Request.MapPath("/") + "sitemape_posts.xml");
            return Content(this.xmlDocument.InnerXml);
        }

        public ActionResult Categories()
        {
            XmlDocument XDOC = new XmlDocument();
            // Create Pages
            XmlElement XDOCsiteMape = XDOC.CreateElement("siteMape");
            this._db.Categories.OrderBy(cat => cat.name).ToList<Category>().ForEach(C =>
            {
                XmlElement post = XDOC.CreateElement("siteMapNode");

                post.SetAttribute("key", "Category");
                post.SetAttribute("url", "/Category/" + C.id);
                post.SetAttribute("title", C.name);
                XDOCsiteMape.AppendChild(post);
            });
            XDOC.AppendChild(XDOCsiteMape);
            XDOC.Save(Request.MapPath("/") + "sitemape_categories.xml");
            // Create Main Posts Pages SiteMape

            return Content(XDOC.InnerXml);
        }

        public ActionResult Tags()
        {
            int tagsCount = this._db.Tags.Select(T => T.Id).Count();
            //int pages = (int)Math.Ceiling((double)(postsCount / 1000));
            XmlElement mainPostsSiteMape = this.xmlDocument.CreateElement("siteMape");
            for (int i = 0; i < tagsCount; i += 1000)
            {
                XmlDocument XDOC = new XmlDocument();
                // Create Pages
                XmlElement XDOCsiteMape = XDOC.CreateElement("siteMape");
                this._db.Tags.OrderBy(tag => tag.Id).Skip(i).Take(i + 1000).ToList<Tag>().ForEach(T =>
                {
                    XmlElement post = XDOC.CreateElement("siteMapNode");

                    post.SetAttribute("key", "Tag");
                    post.SetAttribute("url", "/tag/" + T.Id);
                    post.SetAttribute("title", T.Name);
                    XDOCsiteMape.AppendChild(post);
                });
                XDOC.AppendChild(XDOCsiteMape);
                XDOC.Save(Request.MapPath("/") + "sitemape_tags_P" + (i + 1) + ".xml");
                // Create Main Posts Pages SiteMape

                XmlElement postPage = xmlDocument.CreateElement("siteMapNode");

                postPage.SetAttribute("key", "siteMape");
                postPage.SetAttribute("url", "/" + "sitemape_tags_P" + (i + 1) + ".xml");
                postPage.SetAttribute("title", "sitemape tags page " + (i + 1));
                mainPostsSiteMape.AppendChild(postPage);
            }
            this.xmlDocument.AppendChild(mainPostsSiteMape);
            this.xmlDocument.Save(Request.MapPath("/") + "sitemape_tags.xml");
            return Content(this.xmlDocument.InnerXml);
        }

        public ActionResult Media()
        {
            int mediaCount = this._db.Medias.Select(M => M.Id).Count();
            //int pages = (int)Math.Ceiling((double)(postsCount / 1000));
            XmlElement mainPostsSiteMape = this.xmlDocument.CreateElement("siteMape");
            for (int i = 0; i < mediaCount; i += 1000)
            {
                XmlDocument XDOC = new XmlDocument();
                // Create Pages
                XmlElement XDOCsiteMape = XDOC.CreateElement("siteMape");
                this._db.Medias.OrderBy(M => M.Id).Skip(i).Take(i + 1000).ToList<Media>().ForEach(mItem =>
                {
                    XmlElement media = XDOC.CreateElement("siteMapNode");

                    media.SetAttribute("key", mItem.Type);
                    media.SetAttribute("url", mItem.Uri);
                    media.SetAttribute("title", mItem.Alt);
                    media.SetAttribute("alt", mItem.Alt);
                    XDOCsiteMape.AppendChild(media);
                });
                XDOC.AppendChild(XDOCsiteMape);
                XDOC.Save(Request.MapPath("/") + "sitemape_medias_P" + (i + 1) + ".xml");
                // Create Main Posts Pages SiteMape

                XmlElement postPage = xmlDocument.CreateElement("siteMapNode");

                postPage.SetAttribute("key", "siteMape");
                postPage.SetAttribute("url", "/" + "sitemape_medias_P" + (i + 1) + ".xml");
                postPage.SetAttribute("title", "sitemape Media page " + (i + 1));
                mainPostsSiteMape.AppendChild(postPage);
            }
            this.xmlDocument.AppendChild(mainPostsSiteMape);
            this.xmlDocument.Save(Request.MapPath("/") + "sitemape_medias.xml");
            return Content(this.xmlDocument.InnerXml);
        }
    }
}