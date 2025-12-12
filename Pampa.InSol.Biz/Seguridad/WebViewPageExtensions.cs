using Pampa.InSol.Biz.Contratos.Seguridad;
using System;
using System.Web.Mvc;

namespace Pampa.InSol.Biz.Seguridad
{
    public static class WebViewPageExtensions
    {
        public static MvcHtmlString IfAllowed(this WebViewPage view, int functionId, MvcHtmlString html)
        {
            try
            {
                IAuthorizedModel model = (IAuthorizedModel)view.Model;
                return model.IfAllowed(functionId, view.Context.User, html);
            }
            catch (InvalidCastException icex)
            {
                throw new NotImplementedException(string.Format("El modelo '{0}' debe implementar la interface 'Pampa.Mvc.Extensions.Interfaces.IAuthorizedModel' para poder utilizar esta extensión.", view.Model.GetType().FullName), icex);
            }
        }

        public static bool IsAllowed(this WebViewPage view, int functionId)
        {
            IAuthorizedModel model = (IAuthorizedModel)view.Model;
            return !MvcHtmlString.IsNullOrEmpty(model.IfAllowed(functionId, view.Context.User, new MvcHtmlString("<p></p>")));
        }

        /// <summary>
        /// Devuelve el nombre del controlador que contiene la acción que está asociada a la vista actual.
        /// </summary>
        /// <param name="view"></param>
        /// <returns>Un string con el nombre del controlador</returns>
        public static string CurrentController(this WebViewPage view)
        {
            return view.ViewContext.RouteData.Values["controller"].ToString();
        }
    }
}
