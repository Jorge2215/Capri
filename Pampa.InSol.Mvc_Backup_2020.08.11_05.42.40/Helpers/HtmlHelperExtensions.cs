using System.Web.Mvc;

namespace Pampa.InSol.Mvc.HtmlHelpers
{
    public static partial class HtmlHelperExtensions
    {
        public static string ActivePage(this HtmlHelper helper, string controller, string action)
        {
            var classValue = string.Empty;

            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (currentController == controller && currentAction == action)
            {
                classValue = "active";
            }

            return classValue;
        }
    }
}