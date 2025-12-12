using System.Collections.Generic;
using System.Linq;

namespace Pampa.InSol.Common
{
    /// <summary>
    /// El usuario actualmente autenticado en la session que se encuentra 
    /// disponible para cualquier capa en la arquitectura
    /// </summary>
    public class UsuarioActual
    {
        public UsuarioActual(int id, string userName, string fullname, List<int> functionality, bool active)
        {
            this.Id = id;
            this.UserName = userName;
            this.Fullname = fullname;
            this.UserFunctionality = functionality;
            this.Active = active;
        }

        public virtual int Id { get; private set; }

        public virtual string UserName { get; private set; }

        public string Fullname { get; private set; }

        public List<int> UserFunctionality { get; private set; }

        public bool Active { get; private set; }

        public bool HasPermission(int functionality)
        {
            return this.UserFunctionality.Any(x => x == functionality);
        }
    }
}
