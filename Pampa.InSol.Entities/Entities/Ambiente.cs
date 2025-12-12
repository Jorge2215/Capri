using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Entities
{
    [Table("Ambiente")]
    public class Ambiente : BaseEntity
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ambiente()
        {
            //ProductoAmbiente = new List<ProductoAmbiente>();
        }

        public string Descripcion { get; set; }

        public int Orden { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual List<ProductoAmbiente> ProductoAmbiente { get; set; }
    }
}
