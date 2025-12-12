using NLog;
using Pampa.InSol.Biz.Recursos;
using Pampa.InSol.Common.Utils;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Security;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Pampa.InSol.Biz.Seguridad.Attributes
{
    /// <summary>
    /// Filtro de autorización para cualquier acción de MVC.
    /// </summary>
    public class PampaActionAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private static Logger logger = LogManager.GetLogger(EnumUtils.LOGNAME);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionId">ID de la función que está relacionada a la acción que se está decorando.</param>
        public PampaActionAuthorizeAttribute(int functionId)
        {
            this.FunctionId = functionId;
        }

        /// <summary>
        /// ID de la función a la cual se verificará si el usuario tiene permisos o no.
        /// </summary>
        public int FunctionId
        {
            get;
            protected set;
        }

        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                //TODO: Comentado hasta definición de Roles y Funciones

                // if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                //    throw new SecurityException("Usuario no autenticado.");
                string name = filterContext.HttpContext.User.Identity.Name;
                //name = Security.Instance.CurrentUser.UserLogin.UsuarioNT;
                this.IsAuthorized(name);

                if (filterContext.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items[EnumUtils.USER_KEY] == null ||
                    filterContext.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items[EnumUtils.FUNCTION_ITEM_KEY] == null)
                {
                    Tuple<Usuario, IEnumerable<int>> userFunctions = Security.GetPermission();
                    filterContext.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items.Add(EnumUtils.USER_KEY, userFunctions.Item1);
                    filterContext.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items.Add(EnumUtils.FUNCTION_ITEM_KEY, userFunctions.Item2);
                }

                bool allowed = Security.HasPermission((Usuario)filterContext.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items[EnumUtils.USER_KEY]
                        , (IEnumerable<int>)filterContext.RequestContext.HttpContext.Request.RequestContext.HttpContext.Items[EnumUtils.FUNCTION_ITEM_KEY],
                        FunctionId);

                if (!allowed)
                {
                    throw new UserHasNotPermissionException(
                               string.Format(
                               ErrorMessages.UserError,
                               name));
                }
            }
            catch (SecurityException ex)
            {
                logger.Error(ex.error);
                throw;
            }
        }

        #endregion

        protected void IsAuthorized(string usrLogin)
        {
            PampaAuthorizationCore.IsAuthorized(usrLogin, this.FunctionId);
        }
    }
}
