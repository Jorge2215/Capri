namespace Pampa.InSol.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Roles = new HashSet<Rol>();
        }

        [Required]
        [StringLength(255)]
        public string UsuarioNT { get; set; }

        [StringLength(255)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Apellido { get; set; }

        public bool Activo { get; set; }

        [StringLength(255)]
        public string CreadoPor { get; set; }

        public DateTime FechaCreacion { get; set; }

        [StringLength(255)]
        public string ModificadoPor { get; set; }

        public DateTime FechaModificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rol> Roles { get; set; }
    }
}
