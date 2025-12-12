using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Entities
{
    [Table("Interfaz")]
    public class Interfaz : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Interfaz()
        {
            BitacoraInterfaz = new List<BitacoraInterfaz>();
        }

        public string Nombre { get; set; }
        public int? CicloAplicativoId { get; set; }
        public string Dato { get; set; }
        public int? TipoDatoId { get; set; }
        public int FrecuenciaId { get; set; }
        public int TipoInterfazId { get; set; }
        public int IdProductoOrigen { get; set; }
        public int? IdModuloOrigen { get; set; }
        public int IdProductoDestino { get; set; }
        public int? IdModuloDestino { get; set; }
        public int TecnologiaId { get; set; }
        public int TransporteId { get; set; }
        public bool Activo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }

        [ForeignKey("IdModuloOrigen")]
        public virtual Modulo ModuloOrigen { get; set; }

        [ForeignKey("IdModuloDestino")]
        public virtual Modulo ModuloDestino { get; set; }

        [ForeignKey("IdProductoOrigen")]
        public virtual Producto ProductoOrigen { get; set; }

        [ForeignKey("IdProductoDestino")]
        public virtual Producto ProductoDestino { get; set; }

        public virtual CicloAplicativo CicloAplicativo { get; set; }
        public virtual Tecnologia Tecnologia { get; set; }
        public virtual TipoInterfaz TipoInterfaz { get; set; }
        public virtual Frecuencia Frecuencia { get; set; }
        public virtual Transporte Transporte { get; set; }
        public virtual TipoDato TipoDato { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<BitacoraInterfaz> BitacoraInterfaz { get; set; }
    }
}
