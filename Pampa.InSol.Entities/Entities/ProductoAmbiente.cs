using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Entities
{
    [Table("ProductoAmbiente")]
    public class ProductoAmbiente : BaseEntity
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public ProductoAmbiente()
        //{
        //    //Ambientes = new List<Ambiente>();
        //    //RolesAmbientes = new List<RolAmbiente>();
        //    //Productos = new List<Producto>();
        //}
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

        [ForeignKey("IdAmbiente")]
        public virtual Ambiente Ambiente { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
        [ForeignKey("IdRolAmbiente")]
        public virtual RolAmbiente RolAmbiente { get; set; }

    }
}
