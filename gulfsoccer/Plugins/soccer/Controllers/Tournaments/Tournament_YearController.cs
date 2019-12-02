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
    public class Tournament_YearController : Controller
    {
        private ApplicationDbContext _db;

        public Tournament_YearController()
        {
            this._db = new ApplicationDbContext();
        }

        public Tournament_YearController(ApplicationDbContext DB)
        {
            this._db = DB;
        }


        public ActionResult Index(string tournament, string year, string view)
        {
            // Tournament Year Teams Statistics Table in S1 & S2

            return View(view);
        }

    }
}