using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Enums
{
    public static class CodigoErrorEnum
    {
        public static Dictionary<string, string> errores = new Dictionary<string, string>()
        {
            { "525", "Usuario no encontrado"},
            { "52e", "Credenciales incorrectas" },
            { "530", "No se permite login en este momento" },
            { "531", "No se permite login en esta terminal"},
            { "532", "Contraseña expirada" },
            { "533", "Cuenta deshabilitada" },
            { "701", "Cuenta expirada"},
            { "773", "Es necesario resetear la contraseña" },
            { "775", "Cuenta de usuario bloqueada" }
        };

        //525​ user not found ​(1317)
        //52e​ invalid credentials ​(1326)
        //530​ not permitted to logon at this time​ (1328)
        //531​ not permitted to logon at this workstation​ (1329)
        //532​ password expired ​(1330)
        //533​ account disabled ​(1331) 
        //701​ account expired ​(1793)
        //773​ user must reset password(1907)
        //775​ user account locked(1909)
    }
}
