using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace gulfsoccer.Handlers
{
    public class SoccerRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SoccerHttpHandler(requestContext);
        }
    }
}