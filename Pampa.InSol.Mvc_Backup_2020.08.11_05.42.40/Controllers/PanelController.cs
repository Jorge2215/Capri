using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class PanelController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    
    }
}