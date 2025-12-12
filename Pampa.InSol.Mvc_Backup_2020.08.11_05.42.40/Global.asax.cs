using NLog;
using Pampa.InSol.Biz.Logica.Validacion;
using Pampa.InSol.Common;
using Pampa.InSol.Common.Extensions;
using Pampa.InSol.Common.Logging;
using Pampa.InSol.Mvc.App_Start;
using Pampa.InSol.Mvc.Code;
using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Net;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pampa.InSol.Mvc
{
    public class MvcApplication : HttpApplication
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbInterception.Add(new LoggingCommandInterceptor());

            // Establezco el delegado para obtener el usuario, 
            // esto facitila la generacion de test unitarios que requieran de un usuario en el contexto
            ContextoUsuarioActual.Init(() => HandlerCurrentUser.GetCurrentUser());

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalNullableModelBinder());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex.InnerException.IsNotNull())
            {
                ex = ex.InnerException;
            }

            logger.Error(ex, "Error no manejado.");
            HttpContext httpContext = HttpContext.Current;
            if (httpContext.IsNotNull())
            {
                RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
                string controllerName = requestContext.RouteData.GetRequiredString("controller");
                IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
                IController controller = factory.CreateController(requestContext, controllerName);
                ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);
                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    if (ex is SecurityException)
                    {
                        requestContext.HttpContext.Response.Clear();
                        requestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        requestContext.HttpContext.Response.Write("Su usuario no tiene permisos para ejecutar la acción solicitada");
                        requestContext.HttpContext.Response.End();
                    }

                    // No hacemos nada para que el error lo atrape el handler global de jquery
                }
                else
                {
                    httpContext.Session["errorData"] = ex;
                    httpContext.Response.Redirect("~/Error");
                }
            }
        }
    }
}