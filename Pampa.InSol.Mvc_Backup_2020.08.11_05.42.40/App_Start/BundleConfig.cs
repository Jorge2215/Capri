using System.Web.Optimization;

namespace Pampa.InSol.Mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sass/site.css"));

            //// Kendo
            bundles.Add(new StyleBundle("~/Content/kendo/2016.2.504/css").Include(
                      "~/Content/kendo/2016.2.504/kendo.common.min.css",
                      "~/Content/kendo/2016.2.504/kendo.mobile.all.min.css",
                      "~/Content/kendo/2016.2.504/kendo.dataviz.min.css",
                      "~/Content/kendo/2016.2.504/kendo.silver.min.css",
                      "~/Content/kendo/2016.2.504/kendo.dataviz.silver.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/kendo/2016.2.504/jszip.min.js",
                      "~/Scripts/kendo/2016.2.504/kendo.all.min.js",
                      "~/Scripts/kendo/2016.2.504/kendo.aspnetmvc.min.js",
                      "~/Scripts/kendo/2016.2.504/kendo.validator.min.js",
                      "~/Scripts/kendo/2016.2.504/cultures/kendo.culture.es-AR.min.js",
                      "~/Scripts/kendo/2016.2.504/messages/kendo.messages.es-AR.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ModalForm").Include(
                        "~/Scripts/ModalForm.js"));

            //// TOM
            bundles.Add(new ScriptBundle("~/bundles/TOM").Include(
                      "~/Scripts/TOM/Common.js",
                      "~/Scripts/TOM/Validacion.js"));

            //// Clear all items from the default ignore list to allow minified CSS and JavaScript 
            //// files to be included in debug mode
            bundles.IgnoreList.Clear();

            //// Add back the default ignore list rules sans the ones which affect minified files and debug mode
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }
    }
}