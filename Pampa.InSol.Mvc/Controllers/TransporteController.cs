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
    public class TransporteController : BaseController
    {
        private readonly ITransporteServicio transporteServicio;

        public TransporteController(ITransporteServicio transporteServicio)
        {
            this.transporteServicio = transporteServicio;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListItemTransporte()
        {
            List<TransporteModel> transportesDB = mapper.Map<List<TransporteModel>>(transporteServicio.GetAll());
            var transportes = transportesDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();

            foreach (var item in transportes)
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