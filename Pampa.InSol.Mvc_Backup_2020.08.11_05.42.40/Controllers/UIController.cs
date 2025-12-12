using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class UIController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GraficoMedicion()
        {
            return this.View();
        }
    }
}