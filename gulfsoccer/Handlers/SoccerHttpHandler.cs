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
            try
            {
                for (int i=0; i < context.Request.Params.Count; i++)
                {

                }
                var controllerName = LocalRequestContext.RouteData.GetRequiredString("controller");
                
                var controller = ControllerBuilder.Current.GetControllerFactory().
                CreateController(LocalRequestContext, controllerName);
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