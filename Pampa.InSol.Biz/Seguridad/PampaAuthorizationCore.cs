using System.Linq;
using System.Security;
using Pampa.InSol.Dal;
using Pampa.InSol.Entities;

namespace Pampa.InSol.Biz.Seguridad
{
    public abstract class PampaAuthorizationCore
    {
        public static void IsAuthorized(string usrLogin, int functionId)
        {
            /// TODO: Refactor por acceso a datos

            using (AppDBContext da = new AppDBContext())
            {
                Usuario usr = da.Usuarios.FirstOrDefault(u => u.UsuarioNT == usrLogin && u.Activo);

                if (usr == null)
                {
                    throw new SecurityException(string.Format("El usuario '{0}' no existe o está deshabilitado.", usrLogin));
                }

                var fs = from r in usr.Roles
                         where r.Funciones.Contains(new Funcion() { Id = functionId }, new FuncionIDComparer())
                           && r.Activo
                         select r.Funciones;

                if (fs.Count() == 0)
                {
                    throw new SecurityException(
                        string.Format(
                        "El usuario '{0}' no tiene permisos para usar la función {1}.",
                        usrLogin,
                        functionId));
                }
            }
        }
    }
}
