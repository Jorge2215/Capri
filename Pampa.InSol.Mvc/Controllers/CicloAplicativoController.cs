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
    public class CicloAplicativoController : BaseController
    {
        private readonly ICicloAplicativoServicio cicloAplicativoServicio;

        public CicloAplicativoController(ICicloAplicativoServicio cicloAplicativoServicio)
        {
            this.cicloAplicativoServicio = cicloAplicativoServicio;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListItemCicloAplicativo()
        {
            List<CicloAplicativoModel> cicloAplicativosDB = mapper.Map<List<CicloAplicativoModel>>(cicloAplicativoServicio.GetAll());
            var cicloAplicativo = cicloAplicativosDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });
            
            foreach (var item in cicloAplicativo)
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