using Pampa.InSol.Biz.Contratos.Servicios;
using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IUsuarioServicio usuarioServicio;

        public MenuController(IUsuarioServicio usuarioServicio)
        {
            this.usuarioServicio = usuarioServicio;
        }

        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var user = this.HttpContext.User.Identity.Name;
            if (this.usuarioServicio.EsAdministrador(user))
            {
                return this.PartialView();
            }

            return null;
        }
    }
}