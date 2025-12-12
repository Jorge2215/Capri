using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Entities.Models;

namespace Pampa.InSol.Mvc.Controllers
{
    public class SitioController : BaseController
    {
        private readonly ISitioServicio _sitioServicio;

        public SitioController(ISitioServicio sitioServicio)
        {
            _sitioServicio = sitioServicio;
        }


        // GET: Sitio
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListItemSitio()
        {
            List<SitioModel> sitioDB = mapper.Map<List<SitioModel>>(_sitioServicio.GetAll());
            var sitios = sitioDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            foreach (var item in sitios)
            {
                list.Add(new GenericDropdownItem()
                {
                    Id = item.Id,
                    Descripcion = item.Descripcion
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}