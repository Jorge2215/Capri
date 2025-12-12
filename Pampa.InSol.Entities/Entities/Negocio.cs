using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pampa.InSol.Entities.Entities
{

    [Table("Negocio")]
    public class Negocio : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Negocio()
        {
            Productos = new List<Producto>();
        }
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Producto> Productos { get; set; }
    }
}
