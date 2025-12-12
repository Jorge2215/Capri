using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()

        {
            this.Logger.Info("Ingreso a home");
            return this.View();
        }

        public ActionResult FlatUi()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return this.View();
        }

        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            var version = this.GetType().Assembly.GetName().Version.ToString();
            return this.PartialView(string.Empty, version);
        }
    }
}
