using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Pampa.InSol.Biz.Seguridad.Attributes;
using Pampa.InSol.Biz.Seguridad;
using Pampa.InSol.Entities.Models;
using Pampa.InSol.Entities;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Pampa.InSol.Biz.Contratos.Servicios;

namespace Pampa.InSol.Mvc.Controllers
{
    public class RolFuncionesController : BaseController
    {
        private readonly IRolServicio rolServicio;

        public RolFuncionesController(IRolServicio rolServicio)
        {
            this.rolServicio = rolServicio;
        }

        [PampaActionAuthorize((int)Security.Funciones.Consulta_de_Roles)]
        public ActionResult Index()
        {
            return View();
        }

        [PampaActionAuthorize((int)Security.Funciones.Consulta_de_Roles)]
        public JsonResult Roles_Read([DataSourceRequest]DataSourceRequest request, RolFilterViewModel filterModel)
        {
            List<RolViewModel> usuarios = mapper.Map<List<RolViewModel>>(rolServicio.GetAll(r => 
                                                                    (string.IsNullOrEmpty(filterModel.Descripcion) || r.Descripcion.Contains(filterModel.Descripcion))
                                                                    && (!filterModel.SoloActivos || r.Activo)));
            return Json(usuarios.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [PampaActionAuthorize((int)Security.Funciones.ABM_de_Rol)]
        public ActionResult NuevoRol()
        {
            RolViewModel rvm = new RolViewModel();
            rvm.Activo = true;
            return View("NuevoRol", rvm);
        }

        
        [PampaActionAuthorize((int)Security.Funciones.ABM_de_Rol)]
        public ActionResult Editar(int id)
        {
            RolViewModel rvm = mapper.Map<RolViewModel>(rolServicio.GetOne(id));

            return PartialView("NuevoRol", rvm);
        }

        [HttpPost]
        [PampaActionAuthorize((int)Security.Funciones.ABM_de_Rol)]
        public JsonResult SaveRol(RolViewModel rolModel, List<int> funciones)
        {
            try
            {
                rolServicio.Save(rolModel, funciones);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ThrowJsonError(ex);
            }
        }

        [ActionName("Funciones_Read")]
        public JsonResult Funciones_Read([DataSourceRequest] DataSourceRequest request, int? idRol = null)
        {
            List<FuncionViewModel> results = rolServicio.GetFunciones(idRol);
            return Json(results.ToTreeDataSourceResult(request, e => e.Id, e => e.IdPadre, e => e), JsonRequestBehavior.AllowGet);
        }
    }
}
