using DAL.Database;
using gulfsoccer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace gulfsoccer.Areas.Admin.Controllers
{
    [System.Web.Mvc.Route("/Admin/MultiSelect/")]
    public class MultiSelectController : Controller
    {

        private readonly ApplicationDbContext _db;
        //private List<KendoSelectOption> options = new List<KendoSelectOption>() { new KendoSelectOption { ID = "1", Name = "one" }, new KendoSelectOption { ID = "2", Name = "Two" }, new KendoSelectOption { ID = "3", Name = "Three" } };
        
        public MultiSelectController()
        {
            this._db = new ApplicationDbContext();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(FormCollection models)
        {
            //JArray options = JArray.Parse(models[0]);
            // var option = options[0];
            // var products = JsonConvert.DeserializeObject<IEnumerable<Tag>>(models.ToString());

            // IEnumerable<KendoSelectOption> options = JsonConvert.DeserializeObject<IEnumerable<KendoSelectOption>>(models[0]);
            Models JModel = JsonConvert.DeserializeObject<Models>(models[0]);
            // List<Tag> tags = new List<Tag>();
            foreach(KendoSelectOption option in JModel.models)
            {
                Tag T = new Tag { Id = int.Parse(option.ID), Name = option.Name};
                this._db.Tags.Add(T);
                this._db.SaveChanges();
                option.ID = T.Id.ToString();
            }

            // String result = "[";
            // tags.ForEach(T =>
            // {
                
                // result += "{\"ID\": " + T.Id + " , \"Name\": \"" + T.Name + "\"}";
            // });
            // result += "]";

            // var result = "{models: [{\"ID\": " + 12 +" , \"Name\": \"" + tag.Name + "\"}]}";

            return Json(JModel, JsonRequestBehavior.AllowGet);
            // string content = JsonConvert.SerializeObject(result);
            // return Json(tags);
            // Console.WriteLine(Json(tags, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet));
            // return Json( tags, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectValues(string data)
        {
            string result = "[";
            if (!String.IsNullOrEmpty(data))
            {
                List<string> values = data.Split(',').ToList<string>();
                this._db.Tags.Where(T => values.Contains(T.Id.ToString())).ToList().ForEach(R =>
                {
                    if (result != "[")
                    {
                        result += ", ";
                    }
                    result += "{ \"ID\": " + R.Id + ", \"Name\":\"" + R.Name + "\" }";
                });
            }
            
            result += "]";
            return Content(result);
        }
        // [System.Web.Mvc.Route("/Admin/MultiSelect/ServerFiltering_GetProducts")]
        [System.Web.Mvc.HttpPost]
        public JsonResult ServerFiltering_GetProducts(FormCollection formCollection)
        {

            //var options = new List<KendoSelectOption>();

            //using (ApplicationDbContext context = new ApplicationDbContext()) {
            //    foreach(var tag in context.Tags.Where(T => T.Name.Contains(text))){
            //        options.Add(new KendoSelectOption { ID = tag.Id.ToString(), Name = tag.Name });
            //    }
            //}

            //northwind.Products.Select(product => new ProductViewModel
            //{
            //    ProductID = product.ProductID,
            //    ProductName = product.ProductName,
            //    UnitPrice = product.UnitPrice ?? 0,
            //    UnitsInStock = product.UnitsInStock ?? 0,
            //    UnitsOnOrder = product.UnitsOnOrder ?? 0,
            //    Discontinued = product.Discontinued
            //});
            
            string text = formCollection["text"] != null? formCollection["text"].Replace("\"", ""): "";
            string val = formCollection["val"] != null ? formCollection["val"].Replace("\"", "").Replace("[", "").Replace("]", "") : "";  //val.Replace("\"", "").Replace("[", "").Replace("]", "");

            JsonResult jsn = Json(getOptions(text, val));
            return jsn;
        }



        private List<KendoSelectOption> getOptions(string filter, string val)
        {
            List<KendoSelectOption> options = new List<KendoSelectOption>();
            if (!string.IsNullOrEmpty(filter) || !string.IsNullOrEmpty(val)) {

                CultureInfo ci = new CultureInfo("en-US");
                //var tags = this._db.Tags.Where(T => val.Contains(T.Id.ToString())).ToList();
                //foreach (Tag v in tags)
                //{
                //        options.Add(new KendoSelectOption { ID = v.Id.ToString(), Name = v.Name});
                //}
                var tags = this._db.Tags.Where(T => T.Name.ToLower().StartsWith(filter.ToLower()) /*|| val.Contains(T.Id.ToString())*/).ToList();
                foreach (Tag t in tags)
                {
                    options.Add(new KendoSelectOption { ID = t.Id.ToString(), Name = t.Name });
                }
            }
            return options;
        }


    }

    public class Models
    {
        public IEnumerable<KendoSelectOption> models { get; set; }
    }
    public class KendoSelectOption
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class readViewModel
    {
        public string text { get; set; }
        public string val { get; set; }
    }
}