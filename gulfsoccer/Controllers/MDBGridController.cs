using DAL.Database;
using gulfsoccer.Models;
using gulfsoccer.utilities.MDB;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace gulfsoccer.Controllers
{
    public class MDBGridController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MDBGridController()
        {
            this._db = new ApplicationDbContext();
        }

        // GET: MDBGrid
        [HttpPost]
        public ActionResult GetPageGrids(IEnumerable<MDBGridViewModel> grids)
        {
            List<Grid> MdbGridList = new List<Grid>();
            //var testCards = new List<Card> {
            //        new Card {
            //            articleLink = "/",
            //            title = "Article Title",
            //            imgAlt="",
            //            imgSrc="https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20%28131%29.jpg",
            //            categoryTxt = "Category 1",
            //            categoryLink="",
            //            dark=false,
            //            excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
            //            lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
            //            readMoreText = "Read More >",
            //            writerLink ="/",
            //            writerName = "Writer",
            //            type="CardColumnPostWithCategory"
            //        },
            //        new Card {
            //            articleLink = "/",
            //            title = "Article Title",
            //            imgAlt="",
            //            imgSrc="https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20%28131%29.jpg",
            //            categoryTxt = "Category 1",
            //            categoryLink="",
            //            dark=true,
            //            excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
            //            lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
            //            readMoreText = "Read More >",
            //            writerLink ="/",
            //            writerName = "Writer",
            //            type="CardColumnPostWithCategory"
            //        },
            //        new Card {
            //            articleLink = "/",
            //            title = "Article Title",
            //            imgAlt="",
            //            imgSrc="https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20%28131%29.jpg",
            //            categoryTxt = "Category 1",
            //            categoryLink="",
            //            dark=false,
            //            excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
            //            lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
            //            readMoreText = "Read More >",
            //            writerLink ="/",
            //            writerName = "Writer",
            //            type="CardColumnPostWithCategory"
            //        },
            //        new Card {
            //            articleLink = "/",
            //            title = "Article Title",
            //            imgAlt="",
            //            imgSrc="https://mdbootstrap.com/img/Photos/Others/photo6.jpg",
            //            categoryTxt = "Category 2",
            //            categoryLink="",
            //            dark=false,
            //            excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
            //            lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
            //            readMoreText = "Read More >",
            //            writerLink ="/",
            //            writerName = "Writer",
            //            type="CardColumnPostWithCategory"
            //        },
            //        new Card {
            //            articleLink = "/",
            //            title = "Article Title",
            //            imgAlt="",
            //            imgSrc="https://mdbootstrap.com/img/Photos/Others/photo6.jpg",
            //            categoryTxt = "Category 2",
            //            categoryLink="",
            //            dark=true,
            //            excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
            //            lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
            //            readMoreText = "Read More >",
            //            writerLink ="/",
            //            writerName = "Writer",
            //            type="CardColumnPostWithCategory"
            //        },
            //        new Card {
            //            articleLink = "/",
            //            title = "Article Title",
            //            imgAlt="",
            //            imgSrc="https://mdbootstrap.com/img/Photos/Others/photo6.jpg",
            //            categoryTxt = "Category 2",
            //            categoryLink="",
            //            dark=false,
            //            excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
            //            lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
            //            readMoreText = "Read More >",
            //            writerLink ="/",
            //            writerName = "Writer",
            //            type="CardColumnPostWithCategory"
            //        }
            //};


            grids.ToList().ForEach((grid) =>
            {
                var gridCards = new List<Card>();
                Category gridCategory = _db.Categories.Find(grid.Category);

                _db.Posts.Where(P => _db.PostCategories.Where(PC => PC.CategoryId == grid.Category).Select(PC => PC.PostId).Contains(P.Id) ).ForEach(c => {
                    string ImagePath = _db.Medias.Where(M => M.Id == c.FeaturedImage).SingleOrDefault().Uri;
                    string ImageName = Path.GetFileName(Server.MapPath(ImagePath));
                    string baseDir = "/Content/Thumbnails/" + grid.ThumbSize + "/";
                    string outPath = baseDir + ImageName;
                    Card NC = new Card
                    {
                        articleLink ="/post/" + _db.PermaLinks.Where(p => p.PostId == c.Id).SingleOrDefault().Link,
                        title = c.Title,
                        imgAlt = c.Title,
                        imgSrc = outPath,
                        categoryTxt = _db.Categories.Find(grid.Category).name,
                        categoryLink = "/category/" + grid.Category,
                        dark = false,
                        excerpt = c.Description,
                        lastUpdated = c.Updated.ToString("dd/MM/yyy"),
                        readMoreText = "Read More >",
                        writerLink = "/" + c.Owner,
                        writerName = c.Owner,
                        type = grid.Card
                    };
                    gridCards.Add(NC);
                });
                MdbGridList.Add(new Grid()
                {
                    // Get the category to build the Grid and get Posts to build cards
                    card = grid.Card,
                    headerText = gridCategory.name,
                    gridHeaderLink = "/Category/"+ grid.Category,
                    paragraphTxt = "",
                    type = grid.Type,
                    //cards = testCards.Where(tc => tc.categoryTxt == "Category " + (int.Parse(grid.Elem.Substring(grid.Elem.Length - 1)) + 1)).ToList(),
                    cards = gridCards,
                    element = grid.Elem
                }) ;
            });

            var jsonResult = JsonConvert.SerializeObject(MdbGridList, Formatting.Indented,
                         new JsonSerializerSettings
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
    }

    public class MDBGridViewModel
    {
        public string Type { get; set; }
        public string Card { get; set; }

        public int Category { get; set; }

        public int Cols { get; set; }

        public int Rows { get; set; }
        public string ThumbSize { get; set; }
        public string Elem { get; set; }
    }

    //public class MDBPageGridsViewModel
    //{
    //    public Grid grid { get; set; }
    //    public List<Card> cards { get; set; }
    //}
}