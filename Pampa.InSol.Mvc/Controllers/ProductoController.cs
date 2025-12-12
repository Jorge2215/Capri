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
using Pampa.InSol.Biz.Seguridad;
using Pampa.InSol.Biz.Seguridad.Attributes;

namespace Pampa.InSol.Mvc.Controllers
{
    public class ProductoController : BaseController
    {
        private readonly IProductoServicio productoServicio;
        private readonly IAmbienteServicio ambienteServicio;
        private readonly IServicio<Negocio> negocioServicio;
        private readonly IServicio<Proceso> procesoServicio;

        public ProductoController(IProductoServicio productoServicio
            , IAmbienteServicio ambienteServicio
            , IServicio<Negocio> negocioServicio
            , IServicio<Proceso> procesoServicio)
        {
            this.productoServicio = productoServicio;
            this.ambienteServicio = ambienteServicio;
            this.negocioServicio = negocioServicio;
            this.procesoServicio = procesoServicio;
        }

        [PampaActionAuthorize((int)Security.Funciones.Consulta_Productos)]
        public ActionResult Index()
        {
            return View();
        }

        [PampaActionAuthorize((int)Security.Funciones.Consulta_Productos)]
        public JsonResult Productos_Read([DataSourceRequest]DataSourceRequest request, ProductoModel filterModel)
        {
            var listResult = productoServicio.GetAll(
                u => (string.IsNullOrEmpty(filterModel.Nombre) || u.Nombre.ToLower().Contains(filterModel.Nombre.ToLower()))
                && (filterModel.Id == 0 || u.Id == filterModel.Id)
                && ((filterModel.IdCicloAplicativo == 0 || filterModel.IdCicloAplicativo == null) || filterModel.IdCicloAplicativo == u.IdCicloAplicativo)
                && (u.Activo == filterModel.Activo)
                ).ToList();

            List<ProductoModel> productosModel = mapper.Map<List<ProductoModel>>(listResult);
  

            return Json(productosModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListItemProductos()
        {
            List<ProductoModel> productosDB = mapper.Map<List<ProductoModel>>(productoServicio.GetAll().Where(p => p.Activo == true));
            var productos = productosDB.OrderBy(x => x.Nombre).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            foreach (var item in productos)
            {
                list.Add(new GenericDropdownItem()
                {
                    Id = item.Id,
                    Descripcion = item.Nombre
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListItemProductosPlataforma()
        {
            List<ProductoModel> productosDB = mapper.Map<List<ProductoModel>>(productoServicio.GetAll());
            var productos = productosDB.Where(p => p.EsPlataforma == true).OrderBy(x => x.Nombre).ToList();

            var list = new List<GenericDropdownItem>();
            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            foreach (var item in productos)
            {
                list.Add(new GenericDropdownItem()
                {
                    Id = item.Id,
                    Descripcion = item.Nombre
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //[PampaActionAuthorize((int)Security.Funciones.Producto_ABM)]
        public ActionResult Editar(int id = 0)
        {
            try
            {
                Producto producto;
                ProductoModel productoModel;

                if (id != 0)
                {
                    producto = productoServicio.GetOne(id);
                    productoModel = mapper.Map<ProductoModel>(producto);

                    //Módulos
                    var listaModulosModel = new List<ProductoModuloModel>();
                    var modulos = producto.Modulos;
                    if (modulos != null && modulos.Count > 0)
                    {
                        foreach (var modulo in modulos)
                        {
                            var moduloModel = new ProductoModuloModel();
                            moduloModel.IdProducto = producto.Id;
                            moduloModel.IdModulo = modulo.Id;
                            moduloModel.Nombre = modulo.Nombre;
                            moduloModel.Descripcion = modulo.Descripcion;
                            listaModulosModel.Add(moduloModel);
                        }
                    }
                    ViewBag.listaModulos = listaModulosModel.Select(e => new { e.IdProducto, e.IdModulo, e.Nombre, e.Descripcion }).ToArray();

                    //Ambientes
                    //var ambienteDEV = producto.ProductoAmbiente.Find(x => x.Ambiente.Descripcion == "DEV");
                    //productoModel.AmbienteDEV = mapper.Map<ProductoAmbienteModel>(ambienteDEV);

                    //var ambienteQA = producto.ProductoAmbiente.Find(x => x.Ambiente.Descripcion == "QA");
                    //productoModel.AmbienteQA = mapper.Map<ProductoAmbienteModel>(ambienteQA);

                    //var ambientePROD = producto.ProductoAmbiente.Find(x => x.Ambiente.Descripcion == "PROD");
                    //productoModel.AmbientePROD = mapper.Map<ProductoAmbienteModel>(ambientePROD);
                }
                else
                {
                    productoModel = new ProductoModel()
                    {
                        Id = 0,
                        Activo = true
                    };

                    //var ambientes = ambienteServicio.GetAll().ToList();
                    //productoModel.AmbienteDEV = new ProductoAmbienteModel();
                    //productoModel.AmbienteDEV.IdAmbiente = ambientes.FirstOrDefault(x => x.Descripcion == "DEV").Id;

                    //productoModel.AmbienteQA = new ProductoAmbienteModel();
                    //productoModel.AmbienteQA.IdAmbiente = ambientes.FirstOrDefault(x => x.Descripcion == "QA").Id;

                    //productoModel.AmbientePROD = new ProductoAmbienteModel();
                    //productoModel.AmbientePROD.IdAmbiente = ambientes.FirstOrDefault(x => x.Descripcion == "PROD").Id;
                }

                return this.View("NuevoProducto", productoModel);
            }
            catch (Exception)
            {
                return this.View("Error");
            }
        }

        [HttpPost]
        public JsonResult GetProductoModulos([DataSourceRequest]DataSourceRequest request, int idProducto, List<ProductoModuloModel> listaModulos)
        {
            List<ProductoModuloModel> listaModulosModel = new List<ProductoModuloModel>();

            if (idProducto > 0)
            {
                var producto = productoServicio.GetOne(idProducto);
                var modulosExistentes = producto.Modulos;

                if (modulosExistentes != null && modulosExistentes.Count > 0)
                {
                    foreach (var modulo in modulosExistentes)
                    {
                        var moduloModel = new ProductoModuloModel();
                        moduloModel.IdProducto = producto.Id;
                        moduloModel.IdModulo = modulo.Id;
                        moduloModel.Nombre = modulo.Nombre;
                        moduloModel.Descripcion = modulo.Descripcion;
                        listaModulosModel.Add(moduloModel);
                    }
                }
            }

            if (listaModulos != null && listaModulos.Count > 0)
            {
                foreach (var item in listaModulos)
                {
                    if (!listaModulosModel.Exists(x => x.Nombre == item.Nombre))
                        listaModulosModel.Add(item);
                }
            }
            
            return Json(listaModulosModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListItemModulosByProductos(int? idProducto)
        {
            var list = new List<GenericDropdownItem>();

            if (idProducto != 0)
            {
                var producto = productoServicio.GetOne(idProducto.Value);

                if (producto.Modulos.Count > 0)
                {
                    list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });
                    foreach (var item in producto.Modulos)
                    {
                        list.Add(new GenericDropdownItem()
                        {
                            Id = item.Id,
                            Descripcion = item.Nombre
                        });
                    }
                    list = list.OrderBy(m => m.Descripcion).ToList();
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [PampaActionAuthorize((int)Security.Funciones.Producto_ABM)]
        [HttpPost]
        public JsonResult Save(ProductoModel productoModel, List<ProductoModuloModel> listaModulos, List<ProductoNegocioModel> listaNegocios, List<ProductoProcesoModel> listaProcesos)
        {
            try
            {
                //productoModel.AmbienteDEV = ambienteDev;
                //productoModel.AmbienteQA = ambienteQa;
                //productoModel.AmbientePROD = ambienteProd;
                productoModel.ListaModulos = listaModulos;
                productoModel.ListaNegocios = listaNegocios;
                productoModel.ListaProcesos = listaProcesos;

                //if(listaNegocios.Count()>0)
                //{
                //    productoModel.Negocios = new List<NegocioModel>();
                //foreach (var item in listaNegocios)
                //{
                //    Negocio negocio = this.negocioServicio.GetOne(item.IdNegocio);
                //    NegocioModel negocioModel = new NegocioModel();
                //    negocioModel.Id = negocio.Id;
                //    negocioModel.Descripcion = negocio.Descripcion;
                //    productoModel.Negocios.Add(negocioModel);
                //}
                //}

                Producto producto = productoServicio.Save(productoModel);
                return Json(producto.Id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError(ex);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var result = productoServicio.Delete(id);
                if (result)
                    return Json(new { success = true, message = "Eliminación correcta" }, JsonRequestBehavior.AllowGet);
                else
                {
                    string message = "No se puede eliminar el producto, ya que existen una o más Interfaces asociadas al mismo";
                    return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return ThrowJsonError(ex);
            }
        }

        [PampaActionAuthorize((int)Security.Funciones.Producto_ABM)]
        public void DeleteModuloRelacionado(int idProducto, string nombre)
        {
            productoServicio.DeleteModuloRelacionado(idProducto, nombre);
        }

        public JsonResult GetNegocios(string text)
        {
            List<Negocio> negocios = negocioServicio.GetAll().OrderBy(n => n.Descripcion).ToList();

            if (!string.IsNullOrEmpty(text))
                negocios = negocios.Where(n => n.Descripcion.Contains(text)).ToList();
            return Json(mapper.Map<List<GenericDropdownItem>>(negocios), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProcesosFiltered(int id=0)
        {
            List<Proceso> procesos = procesoServicio.GetAll().OrderBy(n => n.Descripcion).ToList();
            procesos = procesos.Where(p => p.IdCiclo == id).ToList();

            return Json(mapper.Map<List<GenericDropdownItem>>(procesos), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProcesosAll(string text)
        {
            List<Proceso> procesos = procesoServicio.GetAll().OrderBy(n => n.Descripcion).ToList();
            if (!string.IsNullOrEmpty(text))
            {

                procesos = procesos.Where(p => p.IdCiclo.Equals(int.Parse(text))).ToList();
            }
            return Json(mapper.Map<List<GenericDropdownItem>>(procesos), JsonRequestBehavior.AllowGet);
            //return Json(procesos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAmbientes()
        {
            var list = mapper.Map<List<GenericDropdownItem>>(ambienteServicio.GetAll());

            list.Add(new GenericDropdownItem() { Id = 0, Descripcion = "" });

            List<GenericDropdownItem> ambientes = list.OrderBy(a => a.Descripcion).ToList();
            //List<Ambiente> ambientes = ambienteServicio.GetAll().OrderBy(a => a.Descripcion).ToList();
            //List<GenericDropdownItem> list = new List<GenericDropdownItem>();


            return Json(ambientes, JsonRequestBehavior.AllowGet);
        }
     
    }
}