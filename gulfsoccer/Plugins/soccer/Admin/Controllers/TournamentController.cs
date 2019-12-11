using DAL.Database;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Areas.Soccer.Admin.Controllers
{
    public class TournamentController : Controller
    {
        // GET: Admin/Editor
        public ActionResult index(int page=1)
        {
            
            // view Table of all tournaments 
            return View("~/Plugins/soccer/Admin/Views/Tournament/Index.cshtml", 
                        Settings._db.Tournaments.OrderBy(T => T.Name).Take(10 * page).Skip(10 * (page - 1)).ToList()
                   );
        }
        public ActionResult Details(int id)
        {
            // view details of a tournament
            return View("~/Plugins/soccer/Admin/Views/Tournament/Details.cshtml",
                Settings._db.Tournaments.Find(id)
             );
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Create a tournament
            return View("~/Plugins/soccer/Admin/Views/Tournament/Create.cshtml");
        }
        [HttpPost]
        public ActionResult Create(Tournament tournament)
        {
            // Create a tournament
            return RedirectToAction("Edit",new { Id=tournament.Id});
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {

            // Edit  tournament
            return View("~/Plugins/soccer/Admin/Views/Tournament/Edit.cshtml", Settings._db.Tournaments.Find(Id));
        }

        [HttpPost]
        public ActionResult Edit(Tournament tournament)
        {
            // Edit  tournament
            Tournament T = Settings._db.Tournaments.Find(tournament.Id);
            if (T != null && T.Id > 0)
            {
                T.Name = tournament.Name;
                T.Organizer = tournament.Organizer;
                T.Place = tournament.Place;
                Settings._db.SaveChanges();
            }
            return View("~/Plugins/soccer/Admin/Views/Tournament/Edit.cshtml", T);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            // view details of a tournament
            
            return RedirectToAction("Index");
        }
    }
}