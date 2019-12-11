using System.Web.Mvc;

namespace gulfsoccer.Areas.Admin.soccer
{
    public class SoccerAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Soccer/Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Soccer_Admin_default",
                "Soccer/Admin/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "gulfsoccer.Areas.Soccer.Admin.Controllers" }
            );
        }
    }
}