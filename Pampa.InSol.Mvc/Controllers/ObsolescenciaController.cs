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
    public class ObsolescenciaController : BaseController
    {
        private readonly IObsolescenciaServicio _obsolescenciaServicio;

        public ObsolescenciaController(IObsolescenciaServicio obsolescenciaServicio)
        {
            _obsolescenciaServicio = obsolescenciaServicio;
        }
        // GET: Obsolescencia
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListItemObsolescencia()
        {
            List<ObsolescenciaModel> obsolescenciaDB = mapper.Map<List<ObsolescenciaModel>>(_obsolescenciaServicio.GetAll());
            var obsolescencias = obsolescenciaDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            foreach (var item in obsolescencias)
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