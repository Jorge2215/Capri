using Pampa.InSol.Common.Extensions;
using System;
using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            if (this.Session["errorData"].IsNotNull())
            {
                this.View(this.Session["errorData"] as Exception);
                this.Session["errorData"] = null;
            }

            return this.View();
        }

        public ActionResult Test()
        {
            return this.View();
        }

        public ActionResult LanzarError()
        {
            throw new Exception("Excepcion lanzada manualmente para prueba");
        }
    }
}