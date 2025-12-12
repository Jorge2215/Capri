using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pampa.InSol.Common.Extensions
{
    public static partial class ActionLinkExtensions
    {
        private static readonly string ImageLinkHtm = "<img src=\"{0}\" alt=\"{1}\" title=\"{3}\" onclick=\"{2}\" {4} />";
        private static readonly object EmptyObjArray = new object { };

        public static MvcHtmlString ActionImage(this HtmlHelper htmlHelper, string imageUrl, string imageAltText, string actionName, object routeValues)
        {
            return ActionImage(htmlHelper, imageUrl, imageAltText, actionName, routeValues, EmptyObjArray);
        }

        public static MvcHtmlString ActionImage(this HtmlHelper htmlHelper, string imageUrl, string imageAltText, string actionName, object routeValues, object htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                                                    ImageLinkHtm,
                                                    url.Content(imageUrl),
                                                    imageAltText,
                                                    string.Format("window.location.href = '{0}';", url.Action(actionName, routeValues)),
                                                    imageAltText,
                                                    HtmlAttributesToString(htmlAttributes)));
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string imageUrl, string imageAltText, object htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                                                    ImageLinkHtm,
                                                    url.Content(imageUrl),
                                                    imageAltText,
                                                    string.Empty,
                                                    imageAltText,
                                                    HtmlAttributesToString(htmlAttributes)));
        }

        public static MvcHtmlString ConfirmActionImage(this HtmlHelper htmlHelper, string imageUrl, string imageAltText, string questionText, string actionName, object routeValues)
        {
            return ConfirmActionImage(htmlHelper, imageUrl, imageAltText, questionText, actionName, routeValues, EmptyObjArray);
        }

        public static MvcHtmlString ConfirmActionImage(this HtmlHelper htmlHelper, string imageUrl, string imageAltText, string questionText, string actionName, object routeValues, object htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                                                    ImageLinkHtm,
                                                    url.Content(imageUrl),
                                                    imageAltText,
                                                    string.Format("if(confirm('{1}')) window.location.href = '{0}';", url.Action(actionName, routeValues), questionText),
                                                    imageAltText,
                                                    HtmlAttributesToString(htmlAttributes)));
        }

        public static string HtmlAttributesToString(object htmlAttributes)
        {
            RouteValueDictionary rvd = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            StringBuilder attrs = new StringBuilder(string.Empty);

            foreach (string k in rvd.Keys)
            {
                attrs.AppendFormat("{0}=\"{1}\" ", k, rvd[k]);
            }

            return attrs.ToString();
        }

        /// <summary>
        /// Devuelve el nombre de la vista o acción actual.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ViewName(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.GetRequiredString("action");
        }
    }
}
