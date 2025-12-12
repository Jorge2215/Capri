using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Entities
{
    //[Table("ProductoNegocio")]
    public class ProductoNegocio
    {
        public int IdProducto { get; set; }
        public int IdNegocio { get; set; }

        public Producto Producto {get; set;}
        public Negocio Negocio { get; set; }

    }
}
