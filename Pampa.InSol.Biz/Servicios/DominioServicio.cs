using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Common.Extensions;
using Pampa.InSol.Entities.Models;
using System.DirectoryServices;
using System.Web.Hosting;

namespace Pampa.InSol.Biz.Servicios.Negocio
{
    public class DominioServicio : IDominioServicio
    {
        public virtual UsuarioResumido GetInformacionBasicaDeUsuario(string usuarioNTId, string dominio)
        {
            var usuarioResumido = new UsuarioResumido();
            using (HostingEnvironment.Impersonate())
            {
                DirectoryEntry domain = new DirectoryEntry(string.Concat("LDAP://", dominio));
                DirectorySearcher adsearcher = new DirectorySearcher(domain)
                {
                    SearchScope = SearchScope.Subtree,
                    Filter = "(&(objectClass=user)(samaccountname=" + usuarioNTId + "))"
                };
                SearchResult aduser = adsearcher.FindOne();
                if (aduser.IsNull())
                {
                    return null;
                }

                usuarioResumido.Apellido = aduser.Properties["sn"][0].ToString();
                usuarioResumido.Nombre = aduser.Properties["givenname"][0].ToString();
            }

            return usuarioResumido;
        }
    }
}