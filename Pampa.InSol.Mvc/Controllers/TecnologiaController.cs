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
    public class TecnologiaController : BaseController
    {
        private readonly ITecnologiaServicio tecnologiaServicio;

        public TecnologiaController(ITecnologiaServicio tecnologiaServicio)
        {
            this.tecnologiaServicio = tecnologiaServicio;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListItemTecnologia()
        {
            List<TecnologiaModel> tecnologiasDB = mapper.Map<List<TecnologiaModel>>(tecnologiaServicio.GetAll());
            var tecnologias = tecnologiasDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();

            foreach (var item in tecnologias)
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