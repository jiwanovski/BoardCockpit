using System.Web;
using System.Web.Optimization;

namespace BoardCockpit
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862".
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // FileUpload Out
            // bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //             "~/Scripts/jquery.validate*"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Bootstrap metro
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js",
                      "~/Scipts/metro/metro.min.js",
                 "~/Scripts/metro/jquery/jquery.min.js",
                 "~/Scripts/metro/jquery/jquery.widget.min.js",
                 "~/Scripts/metro/jquery/jquery.mousewheel.js",
                //      "~/Scripts/mvcfileupload/blueimp/jquery.min.js",
                //      "~/Scripts/mvcfileupload/blueimp/jquery.widget.min.js",
                //      "~/Scripts/mvcfileupload/blueimp/jquery.mousewheel.js",
                      "~/Scripts/metro/prettify/prettify.js",
                      "~/Scripts/metro/load-metro.js",
                      "~/Scripts/metro/docs.js",
                      "~/Scripts/metro/github.info.js"));
            

            // Bootstrap Metro
            // bundles.Add(new StyleBundle("~/Content/css").Include(
            //           "~/Content/bootstrap.css",
            //           "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(                      
                      "~/Content/metro/metro-bootstrap.css",
                      "~/Content/metro/metro-bootstrap-responsive.css",
                      "~/Content/site.css",
                      "~/Content/metro/iconFont.css",
                      //"~/Content/metro/docs.css",
                      "~/Content/metro/jquery.min.css",
                      "~/Content/metro/jquery.widget.min.css"));

            // Bootstrap Metro
            //bundles.Add(new ScriptBundle("~/bundles/bootstrapMetro").Include(
            //    "~/Scripts/Metro/"));

            // FileUpload
            // bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //             "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //             "~/Scripts/modernizr-*"));

            // bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}
