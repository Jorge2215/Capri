using Pampa.InSol.Biz.Contratos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Pampa.InSol.Biz.Seguridad
{
    public class AuthorizedController : Controller, IAuthorizableController
    {
        private readonly Dictionary<int, bool> authorizationResults = new Dictionary<int, bool>();
        private readonly object lockingToken = new object();

        #region IAuthorizableController Members

        /// <summary>
        /// Devuleve true si el usuario esta autoriazado para el uso de la función
        /// </summary>
        /// <param name="functionId">Identificador de la función</param>
        /// <returns>Devuelve Falso o Verdadero, si el usuario tienen perimsos sobre la función</returns>
        public bool IsUserAuthorized(int functionId)
        {
            lock (this.lockingToken)
            {
                if (!this.HttpContext.User.Identity.IsAuthenticated)
                {
                    return false;
                }

                if (this.authorizationResults.ContainsKey(functionId))
                {
                    return this.authorizationResults[functionId];
                }

                bool res = false;

                try
                {
                    PampaAuthorizationCore.IsAuthorized(this.HttpContext.User.Identity.Name, functionId);
                    res = true;
                }
                catch
                {
                    res = false;
                }

                this.authorizationResults.Add(functionId, res);
                return res;
            }
        }

        #endregion
    }
}
