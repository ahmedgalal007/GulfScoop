// /Tournament/
//            /year:Number
//                 /season:string(S1|S2)
//                               /Week:String(W1|w2|w3...)
//                                           /Match(TeamhomeName-TeamAwayName)String
//                                                  /TeamFormation
//                                                                /Player(MatchStates)
//            /club:String
//                 /Team:string
//                 /Trophy:string
//                 /Match:string
// /club:string
//      /Tournaments
//      /Teams
// /player:string
//        /clubs
//        /matches
//        /cards
//        /injuries
//        /goals

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace gulfsoccer.Handlers
{
    public class SoccerHttpHandler : IHttpHandler
    {
        private RequestContext LocalRequestContext;

        public SoccerHttpHandler(RequestContext requestContext)
        {
            this.LocalRequestContext = requestContext;
        }

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {

            string controllerName = "Home", actionName="Index";
            string[] actionParams = new string[context.Request.Params["Url"].Split('/').Count() -2] ;
            try
            {
                if (String.IsNullOrEmpty(context.Request.Params["Url"]) || context.Request.Params["Url"] == "/")
                {
                    controllerName = "Home"; actionName = "Index";
                }
                else
                {

                    var items = context.Request.Params["Url"].Split('/');
                    if (items[0] != null && Settings.Tournaments.Select(T => T.Name).Contains(items[0]))
                    {

                    }
                    else if (items[0] != null && Settings.Clubs.Select(T => T.Name).Contains(items[0]))
                    {
                        // var controllerName = LocalRequestContext.RouteData.GetRequiredString("controller");

                    }
                    else if (items[0] != null && Settings.Players.Select(T => T.Name).Contains(items[0]))
                    {
                        // var controllerName = LocalRequestContext.RouteData.GetRequiredString("controller");

                    }

                }
                
                var controller = ControllerBuilder.Current.GetControllerFactory().CreateController(LocalRequestContext, controllerName);
                controller.Execute(LocalRequestContext);

                if (controller != null)
                {
                    controller.Execute(LocalRequestContext);
                }

            }
            catch
            {
                try
                {
                    var client = new WebClient();
                    var content = client.DownloadString("http://localhost:24220/" +
                    LocalRequestContext.HttpContext.Request.FilePath);
                    LocalRequestContext.HttpContext.Response.Write(content);
                }
                catch
                {
                    LocalRequestContext.HttpContext.Response.StatusCode = 404;
                }
            }
        }
    }
}