using Pampa.InSol.Biz.Recursos;
using Pampa.InSol.Common.Utils;
using Pampa.InSol.Dal;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Models;
using Pampa.InSol.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Web;

namespace Pampa.InSol.Biz.Seguridad
{
    public class Security
    {
        public enum Funciones
        {
            Seguridad = 60,
            Consulta_Usuarios = 61,
            ABM_de_Usuario = 62,
            Consulta_de_Roles = 63,
            ABM_de_Rol = 64,
            Producto_ABM = 65,
            Consulta_Productos = 66,
            Interfaz_ABM = 67,
            Consulta_Interfaces = 68
        }

        private static Security instance = null;

        public static bool HasPermission(Usuario usr, IEnumerable<int> lFuncionesId, int functionId)
        {
            try
            {
                if (!lFuncionesId.Contains(functionId))
                {
                    throw new UserHasNotPermissionException(
                               string.Format(
                               ErrorMessages.UserSinPermisos2,
                               usr.Nombre,
                               usr.Apellido,
                               functionId));
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected Security()
        {
            using (AppDBContext db = new AppDBContext())
            {
                string userLogin;
#if DEBUG
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    userLogin = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    userLogin = Thread.CurrentPrincipal.Identity.Name.ToUpper();
                }
#else
                userLogin = HttpContext.Current.User.Identity.Name;
#endif

                Usuario user = db.Usuarios.FirstOrDefault(u => u.UsuarioNT == userLogin);
            }
        }

        public static Tuple<Usuario, IEnumerable<int>> GetPermission()
        {
            using (AppDBContext db = new AppDBContext())
            {
                string userLogin = string.Empty;
#if DEBUG
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    userLogin = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    userLogin = Thread.CurrentPrincipal.Identity.Name.ToUpper();
                }
#else
                userLogin = HttpContext.Current.User.Identity.Name;
#endif

                Usuario usr = db.Usuarios.FirstOrDefault(u => u.UsuarioNT == userLogin);

                if (usr == default(Usuario))
                {
                    throw new InvalidUserException(string.Format(ErrorMessages.UserInexistente));
                }

                var fs = (from r in usr.Roles
                          where r.Activo
                          select r.Funciones);

                if (fs.Count() == 0)
                {
                    throw new UserHasNotPermissionException(
                        string.Format(
                        ErrorMessages.UserSinPermisos,
                        usr.Nombre,
                        usr.Apellido));
                }

                List<int> permisos = new List<int>();
                foreach (var f in fs)
                {
                    permisos.AddRange(f.Select(fun => fun.Id));
                }

                return new Tuple<Usuario, IEnumerable<int>>(usr, permisos.Distinct());
            }
        }

        public static Usuario GetUsuario(string userName)
        {
            using (AppDBContext da = new AppDBContext())
            {
                Usuario usr = da.Usuarios.FirstOrDefault(user => user.UsuarioNT.ToLower().Equals(userName.ToLower()));

                if (usr == default(Usuario))
                {
                    throw new InvalidUserException(string.Format(ErrorMessages.UserInexistente));
                }
                return usr;
            }
        }

        public static string UserNT
        {
            get
            {
                Usuario usr = (Usuario)HttpContext.Current.Items[EnumUtils.USER_KEY];
                return HttpContext.Current.User.Identity.Name.ToUpper();
            }
        }

        public static string FullNameCurrentUser
        {
            get
            {
                Usuario usr = (Usuario)HttpContext.Current.Items[EnumUtils.USER_KEY];
                if (usr != null)
                    return $"{usr.Nombre} {usr.Apellido}";
                else
                    return HttpContext.Current.User.Identity.Name.ToUpper();
            }
        }

        /// <summary>
        /// Devuelve una instancia de la clase con el usuario que representá a las credenciales con las cuales se ejecuta actualmente el código.
        /// </summary>
        public static Security Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Security();
                }

                return instance;
            }
        }
    }

    public static class SecurityExtensions
    {
        public static Usuario CurrentUser(this System.Web.HttpRequestBase request)
        {
            if ((Usuario)request.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items[EnumUtils.USER_KEY] != null)
            {
                Usuario user = (Usuario)request.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items[EnumUtils.USER_KEY];
                return user;
            }

            return null;
        }

        public static bool HasPermission(this System.Web.HttpRequestBase request, int functionId)
        {
            try
            {
                return Security.HasPermission((Usuario)request.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items[EnumUtils.USER_KEY],
                                       (IEnumerable<int>)request.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items[EnumUtils.FUNCTION_ITEM_KEY],
                                       functionId);
                //return true;
            }
            catch
            {
                return false;
            }
        }
    }
}