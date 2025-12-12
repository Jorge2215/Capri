using System.Security.Principal;
using System.Web.Mvc;

namespace Pampa.InSol.Biz.Contratos.Seguridad
{
    public interface IAuthorizedModel
    {
        MvcHtmlString IfAllowed(int functionId, IPrincipal user, MvcHtmlString html);
    }
}
