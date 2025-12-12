using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IUsuarioADServicio
    {
        UsuarioResumido GetUsuarioAD(string objectSid);
        UsuarioResumido GetUsuarioADbyUPN(string userPrincipalName);
    }
}
