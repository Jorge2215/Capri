using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Biz.Seguridad.Attributes;
using Pampa.InSol.Common;
using Pampa.InSol.Entities;
using Pampa.InSol.Mvc.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class UsuarioController : BaseController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUsuarioServicio usuarioServicio;
        private readonly IDominioServicio dominioNegocioServicio;
        private readonly IRolServicio rolEntidadServicio;

        public UsuarioController(
            IDominioServicio dominioNegocioServicio,
            IRolServicio rolEntidadServicio,
            IUsuarioServicio usuarioServicio)
        {
            this.usuarioServicio = usuarioServicio;
            this.rolEntidadServicio = rolEntidadServicio;
            this.dominioNegocioServicio = dominioNegocioServicio;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Details(int id)
        {
            return this.View();
        }

        [HttpGet]
        [PampaActionAuthorizeAttribute((int)DirectoryFunctions.Usuario_Alta)]
        public ActionResult Crear()
        {
            try
            {
                var usr = new Usuario();
                usr.UsuarioNT = string.Empty;
                usr.Rol = new List<Rol>();
                this.FillRolsListViewBag();
                return this.View(usr);
            }
            catch (Exception ex) 
            {
                // throw new HttpException(500, ex.Message + "Error al deshabilitar el usuario.");
                return this.View("Error");
            }
        }

        [HttpPost]
        [PampaActionAuthorizeAttribute((int)DirectoryFunctions.Usuario_Alta)]
        public ActionResult Crear(Usuario user)
        {
            logger.Trace("Creando usuario con Nombre '{0}' y Apellido '{1}'", user.Nombre, user.Apellido);
            var creador = ContextoUsuarioActual.Current;
            user.ModificadoPor = creador.UserName;
            user.FechaCreacion = DateTime.Now;
            user.CreadoPor = creador.UserName;
            user.FechaModificacion = DateTime.Now;
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Trace("Formulario invalido al intentar crear usuario con los errors: '{0}'", this.ObtenerTodosLosErroresDelModelo());
                    return this.View(user);
                }

                // TODO: Refactor por UsuarioModelo
                var roles = Request.Form["rols"] != null ? Request.Form["rols"].Split(',') : Enumerable.Empty<string>();
                this.usuarioServicio.CreateUsuario(user, roles, this.UsuarioActual());

                // TODO: Generar aviso la creación del usuario OK
                return this.RedirectToAction("Index");
            }
            catch (HttpException ex)
            {
                /// TODO: ???
                return this.View("Error");
            }
            catch (Exception ex)
            {
                /// TODO: al llegar a este throw no va a llegar al return View("Error");
                throw new HttpException(500, ex.Message + "Error al deshabilitar el usuario.");

                /// TODO: ???
                /// return this.View("Error");
            }
        }

        [HttpGet]
        [PampaActionAuthorizeAttribute((int)DirectoryFunctions.Usuario_Editar)]
        public ActionResult Editar(int id)
        {
            logger.Trace("Se busca el usuario con ID {0} para edicion", id);
            try
            {
                this.FillRolsListViewBag();
                var usuario = this.usuarioServicio.GetUsuarioPorId(id);
                if (usuario == null)
                {
                    logger.Trace("El usuario buscado para edicio con ID '{0}' no existe", id);
                }

                return this.View(usuario);
            }
            catch (Exception)
            {
                // throw new HttpException(500, ex.Message + "Error al deshabilitar el usuario.");
                return this.View("Error");
            }
        }

        [HttpPost]
        [PampaActionAuthorizeAttribute((int)DirectoryFunctions.Usuario_Editar)]
        public ActionResult Editar(Usuario user)
        {
            if (ModelState.IsValid)
            {
                var roles = Request.Form["rols"] != null ? Request.Form["rols"].Split(',') : Enumerable.Empty<string>();
                this.usuarioServicio.ActualizarUsuario(user, roles, this.UsuarioActual());
                return this.RedirectToAction("Index");
            }

            this.FillRolsListViewBag();
            return this.View(user);
        }

        #region json results

        public JsonResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            var usuarios = this.usuarioServicio.GetUsuariosParaGrilla().ToDataSourceResult(request);
            return this.Json(usuarios);
        }

        [HttpGet]
        public JsonResult ConsultaUsuarioAD(string id)
        {
            Usuario user = new Usuario();
            user.UsuarioNT = id;
            try
            {
                var dominio = WebConfigurationManager.AppSettings["Dominio"].ToString();
                var usuarioResumido = this.dominioNegocioServicio.GetInformacionBasicaDeUsuario(id, dominio);
                user.Apellido = usuarioResumido.Apellido;
                user.Nombre = usuarioResumido.Nombre;
                return this.Json(user, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, ex.Message + "Error al consultar el usuario.");
            }
        }

        [HttpPost]
        [PampaActionAuthorizeAttribute((int)DirectoryFunctions.Usuario_Editar)]
        public JsonResult DisableEnableUser(int id)
        {
            try
            {
                this.usuarioServicio.DisableEnableUser(id);
                return this.Json(new { success = true });
            }
            catch (HttpException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new HttpException(500, "Error al deshabilitar el usuario.");
            }
        }

        #endregion json results

        #region private methods

        private void FillRolsListViewBag()
        {
            ViewBag.RolsList = this.rolEntidadServicio.GetDescripciones();
        }

        #endregion private methods
    }
}
