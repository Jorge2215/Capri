using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Models
{
    public class UsuarioModel
    {
        public UsuarioModel()
        {
            Roles = new SelectRolModel();
        }
        public int Id { get; set; }
        public string UsuarioNT { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Activo { get; set; }

        public SelectRolModel Roles { get; set; }
    }
}
