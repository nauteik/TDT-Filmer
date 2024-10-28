using System.Web;
using System.Web.Optimization;

namespace MovieWebsite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new Bundle("~/Scripts").Include(
                       "~/Scripts/custom.js",
                       "~/Scripts/jquery.js",
                       "~/Scripts/plugins.js",
                       "~/Scripts/plugins2.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      
                      "~/Content/site.css",
                      "~/Content/font.css",
                      "~/Content/plugins.css",
                      "~/Content/style.css"
                      //"~/Content/bootstrap.css"
               ));
        }
    }
}
