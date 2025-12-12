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
    public class InterfazController : BaseController
    {
        private readonly IInterfazServicio interfazServicio;

        public InterfazController(IInterfazServicio interfazServicio)
        {
            this.interfazServicio = interfazServicio;
        }

        [PampaActionAuthorize((int)Security.Funciones.Consulta_Interfaces)]
        public ActionResult Index()
        {
            return View();
        }

        [PampaActionAuthorize((int)Security.Funciones.Consulta_Interfaces)]
        public JsonResult Interfaces_Read([DataSourceRequest]DataSourceRequest request, InterfazModel filterModel)
        {
            var listResult = interfazServicio.GetAll(
                u => (string.IsNullOrEmpty(filterModel.Nombre) || u.Nombre.ToLower().Contains(filterModel.Nombre.ToLower()))
                && (u.Activo == filterModel.Activo)
                && (filterModel.Id == 0 || u.Id == filterModel.Id)
                && ((filterModel.CicloAplicativoId == 0 || filterModel.CicloAplicativoId == null) || filterModel.CicloAplicativoId == u.CicloAplicativoId)
                && (filterModel.IdProductoDestino == 0 || filterModel.IdProductoDestino == u.IdProductoDestino)
                && (filterModel.IdProductoOrigen == 0 || filterModel.IdProductoOrigen == u.IdProductoOrigen)
                && ((filterModel.IdModuloOrigen == 0 || filterModel.IdModuloOrigen == null) || filterModel.IdModuloOrigen == u.IdModuloOrigen)
                && ((filterModel.IdModuloDestino == 0 || filterModel.IdModuloDestino == null) || filterModel.IdModuloDestino == u.IdModuloDestino)
                ).ToList();

            List<InterfazModel> interfacesModel = mapper.Map<List<InterfazModel>>(listResult);

            return Json(interfacesModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InterfacesByProducto_Read([DataSourceRequest]DataSourceRequest request, int idProducto)
        {
            List<InterfazModel> interfacesModel = new List<InterfazModel>();

            var interfacesDB = interfazServicio.GetAll(x => x.IdProductoOrigen == idProducto || x.IdProductoDestino == idProducto).Where(a => a.Activo== true);
            interfacesModel = mapper.Map<List<InterfazModel>>(interfacesDB);

            foreach (var interfaz in interfacesModel)
            {
                if (interfaz.IdProductoDestino == idProducto && interfaz.IdProductoOrigen != idProducto)
                    interfaz.InOut = "IN";
                else if (interfaz.IdProductoOrigen == idProducto && interfaz.IdProductoDestino != idProducto)
                    interfaz.InOut = "OUT";
                else
                    interfaz.InOut = "IN/OUT";
            }


            return Json(interfacesModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [PampaActionAuthorize((int)Security.Funciones.Interfaz_ABM)]
        public ActionResult Editar(int id = 0)
        {
            try
            {
                Interfaz interfaz;
                InterfazModel interfazModel;

                var currentUser = HandlerCurrentUser.GetCurrentUser();

                if (id != 0)
                {
                    interfaz = interfazServicio.GetOne(id);
                    interfazModel = mapper.Map<InterfazModel>(interfaz);

                    //Bitacora
                    var listaBitacoraModel = new List<BitacoraInterfazModel>();
                    var bitacora = interfaz.BitacoraInterfaz;
                    if (bitacora != null && bitacora.Count > 0)
                    {
                        foreach (var item in bitacora)
                        {
                            var bitacoraModel = new BitacoraInterfazModel();
                            bitacoraModel.Id = item.Id;
                            bitacoraModel.InterfazId = item.InterfazId;
                            bitacoraModel.Fecha = item.Fecha;
                            bitacoraModel.IdUsuario = item.IdUsuario;
                            bitacoraModel.Usuario = item.Usuario;
                            bitacoraModel.Comentario = item.Comentario;
                            listaBitacoraModel.Add(bitacoraModel);
                        }
                    }
                    var listaBitacoraModelOrdenada = listaBitacoraModel.OrderBy(x => x.Fecha).ToList();
                    ViewBag.listaBitacora = listaBitacoraModelOrdenada.Select(e => new { e.Id, e.InterfazId, e.Fecha, e.IdUsuario, e.Usuario, e.Comentario}).ToArray();
                }
                else
                {
                    interfazModel = new InterfazModel()
                    {
                        Id = 0,
                        Activo = true
                    };
                }

                ViewBag.UsuarioActualId = currentUser.Id;

                return this.View("NuevaInterfaz", interfazModel);
            }
            catch (Exception)
            {
                return this.View("Error");
            }
        }

        [HttpPost]
        public JsonResult GetBitacora([DataSourceRequest]DataSourceRequest request, int idInterfaz, List<BitacoraInterfazModel> listaBitacora)
        {
            List<BitacoraInterfazModel> listaBitacoraModel = new List<BitacoraInterfazModel>();

            if (idInterfaz > 0)
            {
                var interfaz = interfazServicio.GetOne(idInterfaz);
                var bitacoraExistente = interfaz.BitacoraInterfaz;

                if (bitacoraExistente != null && bitacoraExistente.Count > 0)
                {
                    foreach (var item in bitacoraExistente)
                    {
                        var bitacoraModel = mapper.Map<BitacoraInterfazModel>(item);
                        listaBitacoraModel.Add(bitacoraModel);
                    }
                }
            }

            if (listaBitacora != null)
            {
                foreach (var item in listaBitacora)
                {
                    if (!listaBitacoraModel.Exists(x => x.Comentario == item.Comentario))
                        listaBitacoraModel.Add(item);
                }
            }

            var listaBitacoraModelOrdenada = listaBitacoraModel.OrderBy(x => x.Fecha).ToList();

            return Json(listaBitacoraModelOrdenada.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(InterfazModel interfazModel, List<BitacoraInterfazModel> listaBitacora)
        {
            try
            {
                interfazModel.ListaBitacora = listaBitacora;
                interfazServicio.Save(interfazModel);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError(ex);
            }
        }

        public void Delete(int id)
        {
            interfazServicio.Delete(id);
        }

        public void DeleteComentarioRelacionado(int idInterfaz, int idBitacoraInterfaz)
        {
            interfazServicio.DeleteComentarioRelacionado(idInterfaz, idBitacoraInterfaz);
        }
    }
}