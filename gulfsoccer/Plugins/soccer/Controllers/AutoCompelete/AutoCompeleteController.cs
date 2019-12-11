using gulfsoccer.Models;
using gulfsoccer.Plugins.soccer.Controllers;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace gulfsoccer.Controllers
{
    public class AutoCompeleteController : Controller
    {


        public JsonResult Place_Continent([FromUri]string filter)
        {

            var result = Settings._db.Countries.Where(C => C.Name.Contains(filter)).Select(T => new { ID = T.Id, Name = T.Name }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Place_Country([FromUri]string filter)
        {

            var result = Settings._db.Countries.Where(C => C.Name.Contains(filter)).Select(T => new { ID = T.Id, Name = T.Name }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Place_State([FromUri]string filter)
        {

            var result = Settings._db.Countries.Where(C => C.Name.Contains(filter)).Select(T => new { ID = T.Id, Name = T.Name }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Place_City([FromUri]string filter)
        {

            var result = Settings._db.Countries.Where(C => C.Name.Contains(filter)).Select(T => new { ID = T.Id, Name = T.Name }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Organizer([FromUri]string filter)
        {

            var result = Settings._db.Organizations.Where(C => C.Name.Contains(filter)).Select(T => new { ID = T.Id, Name = T.Name }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}