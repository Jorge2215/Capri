namespace Pampa.InSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rol")]
    public partial class Rol : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rol()
        {
            Funciones = new HashSet<Funcion>();
            Usuarios = new HashSet<Usuario>();
        }

        [StringLength(255)]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        [StringLength(255)]
        public string CreadoPor { get; set; }

        public DateTime FechaCreacion { get; set; }

        [StringLength(255)]
        public string ModificadoPor { get; set; }

        public DateTime FechaModificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funcion> Funciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
