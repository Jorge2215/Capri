using Pampa.InSol.Common.Extensions;
using System;
using System.Web;

namespace Pampa.InSol.Common.Utils
{
    public class WebCache
    {
        public virtual bool IsCurrentUserCached
        {
            get
            {
                this.AreThereSession();
                return HttpContext.Current.Session[CatalogoCalvesSession.CurrentTomUser] != null;
            }
        }

        public virtual UsuarioActual CurrentUser
        {
            get
            {
                this.AreThereSession();
                return HttpContext.Current.Session[CatalogoCalvesSession.CurrentTomUser] as UsuarioActual;
            }
        }

        /// <summary>
        /// Elimina todos los datos de seguridad y menu actuales del usuario de la cache
        /// Utilizado idealmente para cuando se realiza una modificacion en permisos para que la 
        /// aplicacion pueda tomar esos cambios
        /// </summary>
        public virtual void Invalidate()
        {
            this.AreThereSession();
            HttpContext.Current.Session[CatalogoCalvesSession.CurrentTomUser] = null;
            this.AreThereApplication();
        }

        /// <summary>
        /// Elimina todos los datos de seguridad y menu actuales para un usuario
        /// </summary>
        public virtual void InvalidateToUser(string usuarioNT)
        {
            HttpContext.Current.Application[string.Format(CatalogoClavesAplicacion.InvalidateCacheToUser, usuarioNT)] = true;
        }

        public virtual void AddCurrentUser(UsuarioActual currentUser)
        {
            this.AreThereSession();
            HttpContext.Current.Session[CatalogoCalvesSession.CurrentTomUser] = currentUser;
        }

        private void AreThereSession()
        {
            if (HttpContext.Current.Session.IsNull())
            {
                throw new NullReferenceException("Session not detected");
            }
        }

        private void AreThereApplication()
        {
            if (HttpContext.Current.Application.IsNull())
            {
                throw new NullReferenceException("Application not detected");
            }
        }
    }
}
