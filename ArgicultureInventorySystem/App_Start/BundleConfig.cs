using System.Web;
using System.Web.Optimization;

namespace ArgicultureInventorySystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootbox.js",
                "~/Scripts/respond.js",
                "~/Scripts/datatables/jquery.datatables.js",
                "~/Scripts/datatables/datatables.bootstrap.js",
                "~/Scripts/typeahead.bundle.js",
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-lumen.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/typeahead.css",
                "~/Content/themes/base/core.css",
                "~/Content/themes/base/resizable.css",
                "~/Content/themes/base/selectable.css",
                "~/Content/themes/base/accordion.css",
                "~/Content/themes/base/autocomplete.css",
                "~/Content/themes/base/button.css",
                "~/Content/themes/base/dialog.css",
                "~/Content/themes/base/slider.css",
                "~/Content/themes/base/tabs.css",
                "~/Content/themes/base/datepicker.css",
                "~/Content/themes/base/progressbar.css",
                "~/Content/themes/base/theme.css"));
        }
    }
}
