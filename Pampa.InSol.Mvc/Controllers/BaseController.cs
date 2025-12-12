using AutoMapper;
using NLog;
using Pampa.InSol.Infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper mapper;
        public BaseController()
        {
            this.mapper = MapperConfig.Mapper;
        }

        protected Logger Logger
        {
            get
            {
                return LogManager.GetCurrentClassLogger();
            }
        }

        protected string UsuarioActual()
        {
            return HttpContext.User.Identity.Name.Substring(HttpContext.User.Identity.Name.Length - 4, 4);
        }

        protected string ObtenerTodosLosErroresDelModelo()
        {
            var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

            return string.Join(",", errors);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
        public JsonResult ThrowJsonError(Exception e)
        {
            Logger.Error(e, e.StackTrace);

            return ThrowJsonError(e.Message);
        }
        public JsonResult ThrowJsonError(string message)
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            Response.StatusDescription = message;

            return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}