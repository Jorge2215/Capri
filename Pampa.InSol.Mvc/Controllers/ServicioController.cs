using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class ServicioController : BaseController
    {
        private readonly IServicioServicio _servicioServicio;
        public ServicioController(IServicioServicio servicioServicio)
        {
            _servicioServicio = servicioServicio;
        }

        public JsonResult GetListItemServicio()
        {
            List<ServicioModel> servicioDB = mapper.Map<List<ServicioModel>>(_servicioServicio.GetAll());
            var servicios = servicioDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            foreach (var item in servicios)
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