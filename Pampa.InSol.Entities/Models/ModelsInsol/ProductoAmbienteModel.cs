using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Models
{
    public class ProductoAmbienteModel
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdAmbiente { get; set; }
        public int IdRolAmbiente { get; set; }
        public string Server { get; set; }

        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string UsuarioEmer { get; set; }
        public string Comentario { get; set; }
        //public string AppPath { get; set; }
        //public string AppURL { get; set; }
        //public string BaseServer { get; set; }
        //public string BaseNombre { get; set; }
        //public string BaseUsuario { get; set; }
        //public string BasePass { get; set; }
        //public string UsuEmer { get; set; }

        public virtual AmbienteModel Ambiente { get; set; }

        public virtual ProductoModel Producto { get; set; }

        public virtual RolAmbienteModel RolAmbiente { get; set; }
    }
}
