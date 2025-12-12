using Pampa.InSol.Entities.Models;
using System;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IDominioServicio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioNTId"></param>
        /// <param name="dominio"></param>
        /// <returns></returns>
        UsuarioResumido GetInformacionBasicaDeUsuario(string usuarioNTId, string dominio);
    }
}
