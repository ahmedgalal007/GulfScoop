﻿using System.Web.Mvc;
using System.Web.Routing;

namespace gulfsoccer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "Admin",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", area = "Admin", permalink = UrlParameter.Optional },
                namespaces: new string[] { "gulfsoccer.Areas.admin.Controllers" }
            );

            //routes.MapRoute(
            //    name: "Image",
            //    url: "Img/{*uri}",
            //    defaults: new { controller = "Img", action = "Index" },
            //    namespaces: new string[] { "gulfsoccer.Controllers" }
            //);
            routes.MapRoute(
               name: "Profile",
               url: "{month}/{day}/{permalink}",
               defaults: new
               {   controller = "Post", action = "OldPost", permalink = UrlParameter.Optional, },
                    new
                    {
                        // year = @"\d{4}",
                        month = @"\d{2}",
                        day = @"\d{2}"
                    },
               namespaces: new string[] { "gulfsoccer.Controllers" }
           );

            routes.MapRoute(
                name: "Post",
                url: "Post/{permalink}",
                defaults: new { controller = "Post", action = "Index", permalink = UrlParameter.Optional},
                namespaces: new string[] { "gulfsoccer.Controllers" }
            );

            routes.MapRoute(
                name: "Category",
                url: "category/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "gulfsoccer.Controllers" }
            );
            routes.MapRoute(
               name: "Tag",
               url: "tag/{id}",
               defaults: new { controller = "Tag", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "gulfsoccer.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "gulfsoccer.Controllers" }
            );

           // routes.MapRoute(
           //    name: "OldPost",
           //    url: "{*month}/{*day}/{*permalink}",
           //    defaults: new { controller = "Post", action = "OldPost", month = UrlParameter.Optional, day = UrlParameter.Optional, permalink = UrlParameter.Optional },
           //    namespaces: new string[] { "gulfsoccer.Controllers" }
           //);

           
        }
    }
}