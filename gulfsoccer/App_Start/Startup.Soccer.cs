using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gulfsoccer
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureSoccer(IAppBuilder app)
        {
            app.CreatePerOwinContext(Settings.Create);
        }
    }
}