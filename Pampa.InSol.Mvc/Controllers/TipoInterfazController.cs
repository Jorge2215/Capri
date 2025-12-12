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
    public class TipoInterfazController : BaseController
    {
        private readonly ITipoInterfazServicio tipoInterfazServicio;

        public TipoInterfazController(ITipoInterfazServicio tipoInterfazServicio)
        {
            this.tipoInterfazServicio = tipoInterfazServicio;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListItemTipoInterfaz()
        {
            List<TipoInterfazModel> tiposInterfazDB = mapper.Map<List<TipoInterfazModel>>(tipoInterfazServicio.GetAll());
            var tiposInterfaz = tiposInterfazDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();

            foreach (var item in tiposInterfaz)
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