using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Pampa.InSol.Biz.Seguridad.Extensions
{
    public static partial class AuthorizedLinkExtensions
    {
        // Summary:
        //     Returns an anchor element (a element) that contains the virtual path of the
        //     specified action.
        //
        // Parameters:
        //   htmlHelper:
        //     The HTML helper instance that this method extends.
        //
        //   linkText:
        //     The inner text of the anchor element.
        //
        //   actionName:
        //     The name of the action.
        //
        // Returns:
        //     An anchor element (a element).
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The linkText parameter is null or empty.
        public static MvcHtmlString AuthorizedActionLink(this HtmlHelper htmlHelper, int functionId, string linkText, string actionName)
        {
            try
            {
                PampaAuthorizationCore.IsAuthorized(htmlHelper.ViewContext.HttpContext.User.Identity.Name, functionId);
                return htmlHelper.ActionLink(linkText, actionName);
            }
            catch
            {
                return MvcHtmlString.Empty;
            }
        }

        // Summary:
        //     Returns an anchor element (a element) that contains the virtual path of the
        //     specified action.
        //
        // Parameters:
        //   htmlHelper:
        //     The HTML helper instance that this method extends.
        //
        //   linkText:
        //     The inner text of the anchor element.
        //
        //   actionName:
        //     The name of the action.
        //
        //   routeValues:
        //     An object that contains the parameters for a route. The parameters are retrieved
        //     through reflection by examining the properties of the object. The object
        //     is typically created by using object initializer syntax.
        //
        // Returns:
        //     An anchor element (a element).
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The linkText parameter is null or empty.
        public static MvcHtmlString AuthorizedActionLink(this HtmlHelper htmlHelper, int functionId, string linkText, string actionName, object routeValues)
        {
            try
            {
                PampaAuthorizationCore.IsAuthorized(htmlHelper.ViewContext.HttpContext.User.Identity.Name, functionId);
                return htmlHelper.ActionLink(linkText, actionName, routeValues);
            }
            catch
            {
                return MvcHtmlString.Empty;
            }
        }

        // Summary:
        //     Returns an anchor element (a element) that contains the virtual path of the
        //     specified action.
        //
        // Parameters:
        //   htmlHelper:
        //     The HTML helper instance that this method extends.
        //
        //   linkText:
        //     The inner text of the anchor element.
        //
        //   actionName:
        //     The name of the action.
        //
        //   routeValues:
        //     An object that contains the parameters for a route.
        //
        // Returns:
        //     An anchor element (a element).
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The linkText parameter is null or empty.
        public static MvcHtmlString AuthorizedActionLink(this HtmlHelper htmlHelper, int functionId, string linkText, string actionName, RouteValueDictionary routeValues)
        {
            try
            {
                PampaAuthorizationCore.IsAuthorized(htmlHelper.ViewContext.HttpContext.User.Identity.Name, functionId);
                return htmlHelper.ActionLink(linkText, actionName, routeValues);
            }
            catch
            {
                return MvcHtmlString.Empty;
            }
        }

        // Summary:
        //     Returns an anchor element (a element) that contains the virtual path of the
        //     specified action.
        //
        // Parameters:
        //   htmlHelper:
        //     The HTML helper instance that this method extends.
        //
        //   linkText:
        //     The inner text of the anchor element.
        //
        //   actionName:
        //     The name of the action.
        //
        //   controllerName:
        //     The name of the controller.
        //
        // Returns:
        //     An anchor element (a element).
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The linkText parameter is null or empty.
        public static MvcHtmlString AuthorizedActionLink(this HtmlHelper htmlHelper, int functionId, string linkText, string actionName, string controllerName)
        {
            try
            {
                PampaAuthorizationCore.IsAuthorized(htmlHelper.ViewContext.HttpContext.User.Identity.Name, functionId);
                return htmlHelper.ActionLink(linkText, actionName, controllerName);
            }
            catch
            {
                return MvcHtmlString.Empty;
            }
        }

        // Summary:
        //     Returns an anchor element (a element) that contains the virtual path of the
        //     specified action.
        //
        // Parameters:
        //   htmlHelper:
        //     The HTML helper instance that this method extends.
        //
        //   linkText:
        //     The inner text of the anchor element.
        //
        //   actionName:
        //     The name of the action.
        //
        //   routeValues:
        //     An object that contains the parameters for a route. The parameters are retrieved
        //     through reflection by examining the properties of the object. The object
        //     is typically created by using object initializer syntax.
        //
        //   htmlAttributes:
        //     An object that contains the HTML attributes for the element. The attributes
        //     are retrieved through reflection by examining the properties of the object.
        //     The object is typically created by using object initializer syntax.
        //
        // Returns:
        //     An anchor element (a element).
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The linkText parameter is null or empty.
        public static MvcHtmlString AuthorizedActionLink(this HtmlHelper htmlHelper, int functionId, string linkText, string actionName, object routeValues, object htmlAttributes)
        {
            try
            {
                PampaAuthorizationCore.IsAuthorized(htmlHelper.ViewContext.HttpContext.User.Identity.Name, functionId);
                return htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes);
            }
            catch
            {
                return MvcHtmlString.Empty;
            }
        }

        // Summary:
        //     Returns an anchor element (a element) that contains the virtual path of the
        //     specified action.
        //
        // Parameters:
        //   htmlHelper:
        //     The HTML helper instance that this method extends.
        //
        //   linkText:
        //     The inner text of the anchor element.
        //
        //   actionName:
        //     The name of the action.
        //
        //   routeValues:
        //     An object that contains the parameters for a route.
        //
        //   htmlAttributes:
        //     An object that contains the HTML attributes to set for the element.
        //
        // Returns:
        //     An anchor element (a element).
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The linkText parameter is null or empty.
        public static MvcHtmlString AuthorizedActionLink(this HtmlHelper htmlHelper, int functionId, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            try
            {
                PampaAuthorizationCore.IsAuthorized(htmlHelper.ViewContext.HttpContext.User.Identity.Name, functionId);
                return htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes);
            }
            catch
            {
                return MvcHtmlString.Empty;
            }
        }

        /// <summary>
        /// Determina si un usuario tiene permisos o no de realizar la acción provista
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName">Acción de MVC</param>
        /// <returns>TRUE: Si el usuario está autorizado, si no FALSE</returns>
        public static bool IsAuthorized(this HtmlHelper htmlHelper, string actionName)
        {
            return IsAuthorized(htmlHelper, actionName, null);
        }

        /// <summary>
        /// Determina si un usuario tiene permisos o no de realizar la acción del controlador provistos
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName">Acción de MVC</param>
        /// <param name="controllerName">Controlador que implementa la acción</param>
        /// <returns>TRUE: Si el usuario está autorizado, si no FALSE</returns>
        public static bool IsAuthorized(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            ControllerBase controllerBase = string.IsNullOrEmpty(controllerName) ? htmlHelper.ViewContext.Controller : htmlHelper.GetControllerByName(controllerName);
            ControllerContext controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerBase);
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerContext.Controller.GetType());
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

            if (actionDescriptor == null)
            {
                return false;
            }

            FilterInfo filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));
            AuthorizationContext authorizationContext = new AuthorizationContext(controllerContext, actionDescriptor);

            foreach (IAuthorizationFilter authorizationFilter in filters.AuthorizationFilters)
            {
                // El filtro desarrollado arroja una excepción si se prohibe al usuario realizar la acción, pero si está hablitado no arroja ninguna.
                try
                {
                    authorizationFilter.OnAuthorization(authorizationContext);
                }
                catch
                {
                    // Al primero que no autorice, se deniega el permiso.
                    return false;
                }
            }

            // Si se llega aca es pq ningún filtro prohibió al usuario
            return true;
        }

        /// <summary>
        /// Devuelve una instancia del controlador deseado
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="controllerName">Nombre del controlador que se desea obtener</param>
        /// <returns>Una instancia del controlador si el nombre provisto es correcto, si no IndexOutOfRangeException.</returns>
        public static ControllerBase GetControllerByName(this HtmlHelper htmlHelper, string controllerName)
        {
            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = factory.CreateController(htmlHelper.ViewContext.RequestContext, controllerName);

            if (controller == null)
            {
                throw new IndexOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "The IControllerFactory '{0}' did not return a controller for the name '{1}'.", factory.GetType(), controllerName));
            }

            return (ControllerBase)controller;
        }

        public static MvcHtmlString IfAllowed(this HtmlHelper htmlHelper, int functionId, MvcHtmlString html)
        {
            try
            {
                PampaAuthorizationCore.IsAuthorized(htmlHelper.ViewContext.HttpContext.User.Identity.Name, functionId);
                return html;
            }
            catch
            {
                return MvcHtmlString.Empty;
            }
        }
    }
}
