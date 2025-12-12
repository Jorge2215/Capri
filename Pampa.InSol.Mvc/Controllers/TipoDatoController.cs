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
    public class TipoDatoController : BaseController
    {
        private readonly ITipoDatoServicio tipoDatoServicio;

        public TipoDatoController(ITipoDatoServicio tipoDatoServicio)
        {
            this.tipoDatoServicio = tipoDatoServicio;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListItemTipoDato()
        {
            List<TipoDatoModel> tiposDatoDB = mapper.Map<List<TipoDatoModel>>(tipoDatoServicio.GetAll());
            var tiposDato = tiposDatoDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            foreach (var item in tiposDato)
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