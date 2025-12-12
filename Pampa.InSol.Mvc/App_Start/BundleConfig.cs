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

            string kendoVersion = "2020.2.617";

            //// Kendo
            bundles.Add(new StyleBundle("~/Content/kendo/2016.2.504/css").Include(
                      $"~/Content/kendo/{kendoVersion}/kendo.common.min.css",
                      $"~/Content/kendo/{kendoVersion}/kendo.mobile.all.min.css",
                      $"~/Content/kendo/{kendoVersion}/kendo.dataviz.min.css",
                      $"~/Content/kendo/{kendoVersion}/kendo.silver.min.css",
                      $"~/Content/kendo/{kendoVersion}/kendo.dataviz.silver.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      $"~/Scripts/kendo/{kendoVersion}/jszip.min.js",
                      $"~/Scripts/kendo/{kendoVersion}/kendo.all.min.js",
                      $"~/Scripts/kendo/{kendoVersion}/kendo.aspnetmvc.min.js",
                      $"~/Scripts/kendo/{kendoVersion}/kendo.validator.min.js",
                      $"~/Scripts/kendo/{kendoVersion}/cultures/kendo.culture.es-AR.min.js",
                      $"~/Scripts/kendo/{kendoVersion}/messages/kendo.messages.es-AR.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ModalForm").Include(
                        "~/Scripts/ModalForm.js"));

            //// InSol
            bundles.Add(new ScriptBundle("~/bundles/insol").Include(
                      "~/Scripts/insol/Common.js",
                      "~/Scripts/insol/Validacion.js"));

            //// Usuarios
            bundles.Add(new ScriptBundle("~/bundles/Usuario").Include(
                "~/Scripts/insol/seguridad/usuario.js"));

            bundles.Add(new ScriptBundle("~/bundles/NuevoUsuario").Include(
                "~/Scripts/insol/seguridad/newUsuario.js"));

            bundles.Add(new ScriptBundle("~/bundles/Roles").Include(
                "~/Scripts/insol/seguridad/rolFuncion.js"));

            bundles.Add(new ScriptBundle("~/bundles/NewRoles").Include(
                "~/Scripts/insol/seguridad/newRol.js"));

            //// Productos
            bundles.Add(new ScriptBundle("~/bundles/Producto").Include(
                "~/Scripts/insol/Producto/producto.js"));

            bundles.Add(new ScriptBundle("~/bundles/NuevoProducto").Include(
                "~/Scripts/insol/Producto/newProducto.js"));

            //// Interfaces
            bundles.Add(new ScriptBundle("~/bundles/Interfaz").Include(
                "~/Scripts/insol/Interfaz/interfaz.js"));

            bundles.Add(new ScriptBundle("~/bundles/NuevaInterfaz").Include(
                "~/Scripts/insol/Interfaz/newInterfaz.js"));

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