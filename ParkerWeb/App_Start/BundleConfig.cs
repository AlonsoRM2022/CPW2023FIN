using System.Web;
using System.Web.Optimization;

namespace ParkerWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.bundle.js",
                       "~/Scripts/fontawesome/all.min.js",
                       "~/Scripts/loadingoverlay.min.js",
                       "~/Scripts/sweetalert.min.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/complementos").Include(
                        "~/Scripts/fontawesome/all.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.responsive.js",
                        "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/pagination.js",
                        "~/Scripts/pagination.min.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/scripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                "~/Content/DataTables/CSS/jquery.dataTables.css",
                "~/Content/DataTables/CSS/responsive.dataTables.css",
                "~/Content/sweetalert.css",
                "~/Content/jquery-ui.css"
                ));
        }
    }
}
