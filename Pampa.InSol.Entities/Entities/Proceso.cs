namespace Pampa.InSol.Entities.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Proceso")]
    public class Proceso : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proceso()
        {
            Productos = new List<Producto>();
        }
        public string Descripcion { get; set; }
        public int IdCiclo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Producto> Productos { get; set; }
    }
}
