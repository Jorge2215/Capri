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
    public class RolAmbienteController : BaseController
    {
        private readonly IRolAmbienteServicio _rolAmbienteServicio;
        public RolAmbienteController(IRolAmbienteServicio rolAmbienteServicio)
        {
            _rolAmbienteServicio = rolAmbienteServicio;
        }
        public JsonResult GetListItemRolAmbiente()
        {
            List<RolAmbienteModel> rolDB = mapper.Map<List<RolAmbienteModel>>(_rolAmbienteServicio.GetAll());
            var roles = rolDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            foreach (var item in roles)
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