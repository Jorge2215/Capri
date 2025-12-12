using Pampa.InSol.Biz.Logica.Validacion;
using Pampa.InSol.Biz.Servicios.Transaccion;
using Pampa.InSol.Common.Extensions;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pampa.InSol.Biz.Servicios
{
    public abstract class BaseABMServicio<TEntidad, TModeloGrilla, TFiltro> : BaseServicio
        where TEntidad : BaseEntity
        where TModeloGrilla : class
        where TFiltro : IFiltroBusqueda
    {
        public BaseABMServicio(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected IRepository<TEntidad> Repositorio
        {
            get
            {
                return this.UnitOfWork.Repository<TEntidad>();
            }
        }

        public virtual TEntidad GetById(int id)
        {
            return this.Repositorio.GetById(id);
        }

        public List<TModeloGrilla> Buscar(TFiltro filtro)
        {
            var predicado = this.ExpresionDeBusqueda(filtro);
            var list = predicado.IsNotNull() ? this.Repositorio.Queryable().Where(predicado).ToList() : this.Repositorio.GetAll().ToList();
            return list.Select(e => this.MapearModeloDesdeEntidad(e)).ToList();
        }

        protected virtual Expression<Func<TEntidad, bool>> ExpresionDeBusqueda(TFiltro filtro)
        {
            return null;
        }

        protected abstract Validador<TEntidad> ObtenerValidadorEliminar();

        //// <summary>
        //// Hook para mapear manualmente el modelo de la grilla para los resultados de las búsquedas. 
        //// </summary>
        //// <param name="entidad"></param>
        //// <returns></returns>
        protected virtual TModeloGrilla MapearModeloDesdeEntidad(TEntidad entidad)
        {
            if (entidad is TModeloGrilla)
            {
                return entidad as TModeloGrilla;
            }
            else
            {
                throw new NotImplementedException("Debe implementar específicamente MapearModeloDesdeEntidad ya que el Modelo y la Entidad son de diferentes tipos.");
            }
        }

        public void Eliminar(int id)
        {
            TEntidad entidad = this.GetById(id);
            this.ObtenerValidadorEliminar().Validar(entidad);
            this.Repositorio.Delete(entidad);
            this.UnitOfWork.SaveChanges();
        }
    }
}