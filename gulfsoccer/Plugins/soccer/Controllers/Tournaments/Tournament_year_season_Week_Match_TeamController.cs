﻿using gulfsoccer.Models;
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
    public class Tournament_Year_Season_Week_Match_TeamController : Controller
    {
        private ApplicationDbContext _db;

        public Tournament_Year_Season_Week_Match_TeamController()
        {
            this._db = new ApplicationDbContext();
        }

        public Tournament_Year_Season_Week_Match_TeamController(ApplicationDbContext DB)
        {
            this._db = DB;
        }


        public ActionResult Index(string tournament, string year, string season, string week, string match, string team, string view)
        {

            return View(view);
        }

    }
}