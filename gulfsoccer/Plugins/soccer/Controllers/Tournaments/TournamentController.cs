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
    public class TournamentController : Controller
    {
        private ApplicationDbContext _db;

        public TournamentController()
        {
            this._db = new ApplicationDbContext();
        }

        public TournamentController( ApplicationDbContext DB)
        {
            this._db = DB;
        }


        public ActionResult Index(string tournament, string view)
        {
            // get all the years of a Tournament and it's first 4 Teams best players

            //Years

                    // First 4 Teams

                            // Best Players

            return View(view);
        }

    }
}