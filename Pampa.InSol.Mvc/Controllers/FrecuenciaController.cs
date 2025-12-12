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
    public class FrecuenciaController : BaseController
    {
        private readonly IFrecuenciaServicio frecuenciaServicio;

        public FrecuenciaController(IFrecuenciaServicio frecuenciaServicio)
        {
            this.frecuenciaServicio = frecuenciaServicio;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListItemFrecuencia()
        {
            List<FrecuenciaModel> frecuenciasDB = mapper.Map<List<FrecuenciaModel>>(frecuenciaServicio.GetAll());
            var frecuencias = frecuenciasDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();

            foreach (var item in frecuencias)
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