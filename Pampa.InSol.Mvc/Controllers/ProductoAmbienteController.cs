using Pampa.InSol.Biz.Contratos.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Pampa.InSol.Entities.Models;
using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Mvc.Code;
using Pampa.InSol.Biz.Seguridad.Attributes;
using Pampa.InSol.Biz.Seguridad;


namespace Pampa.InSol.Mvc.Controllers
{
    public class ProductoAmbienteController : BaseController
    {
        private readonly IProductoAmbienteServicio _productoAmbienteServicio;
        public ProductoAmbienteController(IProductoAmbienteServicio productoAmbienteServicio)
        {
            _productoAmbienteServicio = productoAmbienteServicio;
        }
        // GET: ProductoAmbiente
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult AmbientesByProducto_Read([DataSourceRequest]DataSourceRequest request, int idProducto)
        {
            try
            {

                var ambientesDB = _productoAmbienteServicio.GetAll(x => x.IdProducto == idProducto).OrderBy(c => c.Ambiente.Orden).ToList();

                List<ProductoAmbienteModel>  ambientesModel = mapper.Map<List<ProductoAmbienteModel>>(ambientesDB);

                return Json(ambientesModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError(ex);
            }
        }

        [HttpPost]
        public JsonResult AmbientesByProducto_Delete([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProductoAmbienteModel> model )
        {
            try
            {
                if (model.Any())
                {
                    var entityDB= _productoAmbienteServicio.GetOne(x => x.Id == model.FirstOrDefault().Id);
                    _productoAmbienteServicio.Delete(entityDB);
                }

                return Json("", JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                return ThrowJsonError(ex);
            }
        }

        public JsonResult AmbientesByProducto_Create([DataSourceRequest]DataSourceRequest request, ProductoAmbienteModel model)
        {
            try
            {
                if (model != null)
                {
                    _productoAmbienteServicio.Save(model);

                }

                var ambientesDB = _productoAmbienteServicio.GetAll(x => x.IdProducto == model.IdProducto).ToList();

                List<ProductoAmbienteModel> ambientesModel = mapper.Map<List<ProductoAmbienteModel>>(ambientesDB);

                return Json(ambientesModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError(ex);
            }
        }
    }
}