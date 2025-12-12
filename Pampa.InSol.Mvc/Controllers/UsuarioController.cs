using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Biz.Seguridad;
using Pampa.InSol.Biz.Seguridad.Attributes;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    //[PampaActionAuthorize((int)Security.Funciones.Seguridad)]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioServicio usuarioServicio;
        private readonly IRolServicio rolServicio;

        public UsuarioController(IUsuarioServicio usuarioServicio, IRolServicio rolServicio)
        {
            this.usuarioServicio = usuarioServicio;
            this.rolServicio = rolServicio;
        }

        //[PampaActionAuthorize((int)Security.Funciones.Consulta_Usuarios)]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        //[PampaActionAuthorizeAttribute((int)Security.Funciones.ABM_de_Usuario)]
        public ActionResult NuevoUsuario()
        {
            UsuarioModel newUsuario = new UsuarioModel();
            newUsuario.Id = 0;
            newUsuario.Nombre = string.Empty;
            newUsuario.Apellido = string.Empty;
            newUsuario.UsuarioNT = string.Empty;
            newUsuario.Activo = true;
            newUsuario.Roles.RolesDisponibles = rolServicio.GetAll().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Descripcion
            }).ToList();
            return View(newUsuario);
        }

        [HttpPost]
        //[PampaActionAuthorize((int)Security.Funciones.ABM_de_Usuario)]
        public JsonResult NuevoUsuario(UsuarioModel newUser, List<int> rolesId)
        {
            Logger.Trace("Creando usuario con Nombre '{0}' y Apellido '{1}'", newUser.Nombre, newUser.Apellido);
            try
            {
                usuarioServicio.Insert(newUser, rolesId);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError(ex);
            }
        }

        [HttpGet]
        //[PampaActionAuthorizeAttribute((int)Security.Funciones.ABM_de_Usuario)]
        public ActionResult Editar(int id)
        {
            Logger.Trace("Se busca el usuario con ID {0} para edicion", id);
            try
            {
                Usuario user = usuarioServicio.GetOne(id);

                UsuarioModel userModel = mapper.Map<UsuarioModel>(user);

                userModel.Roles.RolesAsignados = rolServicio.GetRolesAsignadosByUsuario(user.Id);
                userModel.Roles.RolesDisponibles = rolServicio.GetRolesDisponiblesByUsuario(user.Id);

                return this.View("NuevoUsuario", userModel);

            }
            catch (Exception)
            {
                // throw new HttpException(500, ex.Message + "Error al deshabilitar el usuario.");
                return this.View("Error");
            }
        }

        #region json results

        //[PampaActionAuthorize((int)Security.Funciones.Consulta_Usuarios)]
        public JsonResult Users_Read([DataSourceRequest]DataSourceRequest request, UsuarioModel filterModel)
        {
            //List<Usuario> usuarios = usuarioServicio.GetAll(u => (string.IsNullOrEmpty(filterModel.UsuarioNT) || u.UsuarioNT.ToLower().Contains(filterModel.UsuarioNT.ToLower()))
            //                                && (string.IsNullOrEmpty(filterModel.Nombre) || u.Nombre.Equals(filterModel.Nombre))
            //                                && (string.IsNullOrEmpty(filterModel.Apellido) || u.Apellido.Equals(filterModel.Apellido))
            //                                && (!filterModel.Activo || u.Activo)).ToList();
            //return Json(usuarios.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            List<UsuarioModel> usuarios = mapper.Map<List<UsuarioModel>>(usuarioServicio.GetAll(u => (string.IsNullOrEmpty(filterModel.UsuarioNT) || u.UsuarioNT.ToLower().Contains(filterModel.UsuarioNT.ToLower()))
                                            && (string.IsNullOrEmpty(filterModel.Nombre) || u.Nombre.ToLower().Contains(filterModel.Nombre.ToLower()))
                                            && (string.IsNullOrEmpty(filterModel.Apellido) || u.Apellido.ToLower().Contains(filterModel.Apellido.ToLower()))
                                            && (!filterModel.Activo || u.Activo), orderBy: q => q.OrderBy(x => x.UsuarioNT)));

            return Json(usuarios.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUsuarioAD(string usuarioNT)
        {
            Usuario user = new Usuario();
            user.UsuarioNT = usuarioNT.Trim();
            UsuarioResumido usuarioResumido = usuarioServicio.GetUsuarioAD(usuarioNT.Trim());

            if (usuarioResumido != null)
            {
                user.Apellido = usuarioResumido.Apellido;
                user.Nombre = usuarioResumido.Nombre;

                return this.Json(new { success = true, user }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion json results
    }
}
