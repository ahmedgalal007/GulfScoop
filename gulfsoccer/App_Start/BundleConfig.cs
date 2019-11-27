using System.Web;
using System.Web.Optimization;

namespace gulfsoccer
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/post").Include("~/Scripts/lazysizes.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/modernizr-*","~/Scripts/jquery-{version}.min.js", "~/Scripts/lazysizes.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/kendo/2019.2.619/jquery.min.js",
                      "~/Scripts/kendo/2019.2.619/kendo.all.min.js",
                      // uncomment below if using the Scheduler
                      // "~/Scripts/kendo/2019.2.619/kendo.timezones.min.js",
                      "~/Scripts/kendo/2019.2.619/kendo.aspnetmvc.min.js"
            ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                      , "~/Content/blog.css"));

            bundles.Add(new StyleBundle("~/Content/mdbcss").Include(
                      "~/Content/bootstrap.css"
                      , "~/Content/mdb/css/mdb.css"
                      //, "~/Content/blog.css"
                      //, "~/Content/site.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/mdbprocss").Include(
                     "~/Content/mdbPro/css/bootstrap.css"
                     , "~/Content/mdbPro/css/mdb.css"
                     //, "~/Content/blog.css"
                     , "~/Content/site.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/kendo/css")
                      .Include("~/Content/kendo/2019.2.619/kendo.common.min.css", new CssRewriteUrlTransform())
                      .Include("~/Content/kendo/2019.2.619/kendo.material.min.css", new CssRewriteUrlTransform())
                      .Include("~/Content/kendo/2019.2.619/kendo.material.mobile.min.css", new CssRewriteUrlTransform())
                      //.Include("~/Content/kendo/2019.2.619/kendo.default.min.css", new CssRewriteUrlTransform())
                      //.Include("~/Content/kendo/2019.2.619/kendo.default.mobile.min.css", new CssRewriteUrlTransform())
                      //.Include("~/Content/kendo/2019.2.619/kendo.rtl.min.css", new CssRewriteUrlTransform())
                      );


            bundles.Add(new ScriptBundle("~/bundles/mdb")
                .Include("~/Scripts/mdb/js/jquery.min.js", "~/Scripts/mdb/js/popper.min.js", "~/Scripts/bootstrap.min.js", "~/Scripts/mdb/js/mdb.min.js", "~/Scripts/site/app/app.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/mdbpro")
               .Include("~/Scripts/mdbpro/js/jquery.min.js", "~/Scripts/mdbpro/js/popper.min.js", "~/Scripts/bootstrap.min.js", "~/Scripts/mdbpro/js/mdb.min.js", "~/Scripts/site/app/app.js")
           );



            bundles.IgnoreList.Clear();
        }
    }
}
