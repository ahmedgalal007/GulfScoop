using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gulfsoccer.utilities.MDB;
using Newtonsoft.Json;

namespace gulfsoccer.Controllers
{
    public class MDBGridController : Controller
    {
        // GET: MDBGrid
        [HttpPost]
        public ActionResult GetPageGrids(IEnumerable<MDBGridViewModel> grids)
        {
            List<Grid> MdbGridList = new List<Grid>();
            var testCards = new List<Card> {
                    new Card {
                        articleLink = "/",
                        title = "Article Title",
                        imgAlt="",
                        imgSrc="https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20%28131%29.jpg",
                        categoryTxt = "Category 1",
                        categoryLink="",
                        dark=false,
                        excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
                        lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
                        readMoreText = "Read More >",
                        writerLink ="/",
                        writerName = "Writer",
                        type="CardColumnPostWithCategory"
                    },
                    new Card {
                        articleLink = "/",
                        title = "Article Title",
                        imgAlt="",
                        imgSrc="https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20%28131%29.jpg",
                        categoryTxt = "Category 1",
                        categoryLink="",
                        dark=true,
                        excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
                        lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
                        readMoreText = "Read More >",
                        writerLink ="/",
                        writerName = "Writer",
                        type="CardColumnPostWithCategory"
                    },
                    new Card {
                        articleLink = "/",
                        title = "Article Title",
                        imgAlt="",
                        imgSrc="https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20%28131%29.jpg",
                        categoryTxt = "Category 1",
                        categoryLink="",
                        dark=false,
                        excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
                        lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
                        readMoreText = "Read More >",
                        writerLink ="/",
                        writerName = "Writer",
                        type="CardColumnPostWithCategory"
                    },
                    new Card {
                        articleLink = "/",
                        title = "Article Title",
                        imgAlt="",
                        imgSrc="https://mdbootstrap.com/img/Photos/Others/photo6.jpg",
                        categoryTxt = "Category 2",
                        categoryLink="",
                        dark=false,
                        excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
                        lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
                        readMoreText = "Read More >",
                        writerLink ="/",
                        writerName = "Writer",
                        type="CardColumnPostWithCategory"
                    },
                    new Card {
                        articleLink = "/",
                        title = "Article Title",
                        imgAlt="",
                        imgSrc="https://mdbootstrap.com/img/Photos/Others/photo6.jpg",
                        categoryTxt = "Category 2",
                        categoryLink="",
                        dark=true,
                        excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
                        lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
                        readMoreText = "Read More >",
                        writerLink ="/",
                        writerName = "Writer",
                        type="CardColumnPostWithCategory"
                    },
                    new Card {
                        articleLink = "/",
                        title = "Article Title",
                        imgAlt="",
                        imgSrc="https://mdbootstrap.com/img/Photos/Others/photo6.jpg",
                        categoryTxt = "Category 2",
                        categoryLink="",
                        dark=false,
                        excerpt="Some quick example text to build on the card title and make up the bulk of the card's content.",
                        lastUpdated = DateTime.Now.ToString("dd/MM/yyyy"),
                        readMoreText = "Read More >",
                        writerLink ="/",
                        writerName = "Writer",
                        type="CardColumnPostWithCategory"
                    }
            };

            grids.ToList().ForEach((grid) =>
            {
                MdbGridList.Add(new Grid()
                {
                    // Get the category to build the Grid and get Posts to build cards
                    card = grid.Card ,
                    headerText = "Grid Example !",
                    gridHeaderLink = "/",
                    paragraphTxt = "",
                    type = grid.Type,
                    cards = testCards.Where(tc => tc.categoryTxt == "Category " + (int.Parse(grid.Elem.Substring(grid.Elem.Length - 1 ))+1)).ToList(),
                    element = grid.Elem
                });
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

        public string Elem { get; set; }
    }
    //public class MDBPageGridsViewModel
    //{
    //    public Grid grid { get; set; }
    //    public List<Card> cards { get; set; }
    //}
}