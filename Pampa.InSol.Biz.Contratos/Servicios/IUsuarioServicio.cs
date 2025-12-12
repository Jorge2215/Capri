using Pampa.InSol.Common;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IUsuarioServicio : IServicio<Usuario>, IUsuarioADServicio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<UsuarioGrilla> GetUsuariosParaGrilla();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Usuario GetUsuarioPorId(int id);

        UsuarioActual GetUsuarioActualPorIdRed(string usuarioNT);

        bool EsAdministrador(string idRed);

        Usuario GetUsuarioPorIdRed(string idRed);

        int ObtenerIdUsuarioContextoActual();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="listaDeRoles"></param>
        /// <param name="usuarioActual"></param>
        void CreateUsuario(Usuario usuario, IEnumerable<string> listaDeRoles, string usuarioActual);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="listaDeRoles"></param>
        /// <param name="usuarioActual"></param>
        void ActualizarUsuario(Usuario usuario, IEnumerable<string> listaDeRoles, string usuarioActual);

        void DisableEnableUser(int id);
        UsuarioResumido GetUsuarioAD(string usuarioNTId);

        Usuario Insert(UsuarioModel objeto, List<int> roles);
    }
}