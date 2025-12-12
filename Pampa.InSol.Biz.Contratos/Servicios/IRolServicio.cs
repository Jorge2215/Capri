using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IRolServicio : IServicio<Rol>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDictionary<string, Rol> GetRolesActivos();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<string> GetDescripciones();
        void Save(RolViewModel rolModel, List<int> funciones);

        List<FuncionViewModel> GetFunciones(int? idRol);
        List<SelectListItem> GetRolesAsignadosByUsuario(int idUsuario);
        List<SelectListItem> GetRolesDisponiblesByUsuario(int? idUsuario = null);

    }
}
