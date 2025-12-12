using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Pampa.InSol.Common.Extensions
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString LinkParaBreadcrumb(this HtmlHelper helper, Breadcrumb item, bool activo = false)
        {
            switch (item)
            {
                case Breadcrumb.Home:
                    return helper.ActionLink("Home", "Index", "Home");
                default:
                    throw new ArgumentException("EL ítem del Breadcrumb solicitado no existe");
            }
        }

        public static MvcHtmlString TomValidationSummary(this HtmlHelper helper)
        {
            return helper.Partial("TomValidationSummary");
        }
    }
}