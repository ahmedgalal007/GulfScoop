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
    public class MainController : Controller
    {
        private ApplicationDbContext _db;

        public MainController()
        {
            this._db = new ApplicationDbContext();
        }

        public MainController( ApplicationDbContext DB)
        {
            this._db = DB;
        }


        public ActionResult Index(string url)
        {
            IController controller;
            Settings settings = this.HttpContext.GetOwinContext().Get<Settings>();
            ControllerRedirectContext view = AppRoutesProcessor.GetRoute(url, this._db);

            if (String.IsNullOrEmpty(view.ControllerName) || view.ControllerName== "Home")
            {
                view.ControllerName = "Home";
                
            }
            
            controller = ControllerBuilder.Current.GetControllerFactory().CreateController(Request.RequestContext, view.ControllerName);
            Request.RequestContext.RouteData.Values["controller"] = view.ControllerName;
            Request.RequestContext.RouteData.Values["view"] = view.ViewName;
            foreach (var key in view.RouteValues.Keys)
            {
                Request.RequestContext.RouteData.Values[key] = view.RouteValues[key];
            } 
            controller.Execute(Request.RequestContext);

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);

            // return RedirectToAction("Index", view.ControllerName, view.RouteValues);
            // return View(view.ViewName, view.Modal);



        }

    }
}