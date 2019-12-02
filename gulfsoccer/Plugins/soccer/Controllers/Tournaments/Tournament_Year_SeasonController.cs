using gulfsoccer.Models;
using gulfsoccer.Plugins.soccer.Controllers;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Controllers
{
    public class Tournament_Year_SeasonController : Controller
    {
        private ApplicationDbContext _db;

        public Tournament_Year_SeasonController()
        {
            this._db = new ApplicationDbContext();
        }

        public Tournament_Year_SeasonController(ApplicationDbContext DB)
        {
            this._db = DB;
        }


        public ActionResult Index(string tournament, string year, string season, string view)
        {

            return View(view);
        }

    }
}