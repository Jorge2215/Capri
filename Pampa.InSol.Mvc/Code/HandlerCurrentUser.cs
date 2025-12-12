using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Common;
using Pampa.InSol.Common.Extensions;
using Pampa.InSol.Common.Utils;
using Pampa.InSol.IoC.App_Start;
using System.Net;
using System.Web;

namespace Pampa.InSol.Mvc.Code
{
    public class HandlerCurrentUser
    {
        public static UsuarioActual GetCurrentUser()
        {
            UsuarioActual user = null;
            if ((user = GetCurrentUserCache()) != null)
            {
                return user;
            }

            var usuarioServicio = UnityConfig.Resolve<IUsuarioServicio>();
            if (HttpContext.Current.User.IsNull() || HttpContext.Current.User.Identity.IsNull() || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                return null;
            }

            var usuarioNT = HttpContext.Current.User.Identity.Name;
            var usuarioActual = usuarioServicio.GetUsuarioActualPorIdRed(usuarioNT);
            if (usuarioActual.IsNull())
            {
                throw new HttpException((int)HttpStatusCode.Forbidden, "Su usuario no esta registrado para acceder a la aplicación");
            }

            var cache = new WebCache();
            cache.AddCurrentUser(usuarioActual);
            return usuarioActual;
        }

        /// <summary>
        /// Devuelve el current User cacheado
        /// </summary>
        /// <returns>El usuario actual cacheado, o null si no está cacheado</returns>
        public static UsuarioActual GetCurrentUserCache()
        {
            var cache = new WebCache();
            var user = cache.CurrentUser;
            if (user.IsNull())
            {
                return user;
            }

            bool invalidateCache;
            if (HttpContext.Current.Application[string.Format(CatalogoClavesAplicacion.InvalidateCacheToUser, user.UserName)].IsNotNull())
            {
                bool.TryParse(HttpContext.Current.Application[string.Format(CatalogoClavesAplicacion.InvalidateCacheToUser, user.UserName)].ToString(), out invalidateCache);
                if (invalidateCache)
                {
                    cache.Invalidate();
                    HttpContext.Current.Application[string.Format(CatalogoClavesAplicacion.InvalidateCacheToUser, user.UserName)] = false;
                }
            }

            return user;
        }
    }
}