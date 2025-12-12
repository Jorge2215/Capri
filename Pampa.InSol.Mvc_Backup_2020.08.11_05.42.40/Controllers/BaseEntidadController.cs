using Pampa.InSol.Biz.Contratos.Servicios.Entidad;
using Pampa.InSol.Biz.Logica.Validacion;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Web.Mvc;

namespace Pampa.InSol.Mvc.Controllers
{
    public class BaseEntidadController<TEntidad, TModeloGrilla, TFiltro> : BaseController
        where TEntidad : BaseEntity
        where TModeloGrilla : class
        where TFiltro : IFiltroBusqueda
    {
        private readonly IABMServicio<TEntidad, TModeloGrilla, TFiltro> servicio;

        public BaseEntidadController(IABMServicio<TEntidad, TModeloGrilla, TFiltro> servicio)
        : base()
        {
            this.servicio = servicio;
        }

        protected IABMServicio<TEntidad, TModeloGrilla, TFiltro> Servicio
        {
            get
            {
                return this.servicio;
            }
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Buscar(TFiltro filtro)
        {
            try
            {
                var datos = this.servicio.Buscar(filtro);
                var view = this.RenderPartialViewToString("GrillaPrincipal", datos);
                return this.Json(new { success = true, view = view }, JsonRequestBehavior.AllowGet);
            }
            catch (ValidacionNegocioException ex)
            {
                return this.Json(new { success = false, error = ex.ErroresValidacion }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual ActionResult Eliminar(int id)
        {
            try
            {
                this.Servicio.Eliminar(id);
                return this.Json(new { success = true });
            }
            catch (ValidacionNegocioException ex)
            {
                string errores = string.Empty;
                ex.ErroresValidacion.ForEach(x => errores += x + " ");
                return this.Json(new { success = false, error = errores });
            }
        }
    }
}