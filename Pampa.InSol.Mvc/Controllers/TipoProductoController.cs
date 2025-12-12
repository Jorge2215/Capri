using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class TipoProductoController : BaseController
    {
        private readonly ITipoProductoServicio _tipoProductoServicio;
        public TipoProductoController(ITipoProductoServicio tipoProductoServicio)
        {
            _tipoProductoServicio = tipoProductoServicio;
        }

        public JsonResult GetListItemTipoProducto()
        {
            List<TipoProductoModel> tipoProductoDB = mapper.Map<List<TipoProductoModel>>(_tipoProductoServicio.GetAll());
            var tipoProductos = tipoProductoDB.OrderBy(x => x.Descripcion).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            foreach (var item in tipoProductos)
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