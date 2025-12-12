using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Entities
{
    [Table("Producto")]
    public class Producto : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            //ProductoAmbiente = new List<ProductoAmbiente>();
            Modulos = new List<Modulo>();
            Negocios = new List<Negocio>();
            Procesos = new List<Proceso>();
            //Ambientes = new List<Ambiente>();
            //RolesAmbientes = new List<RolAmbiente>();
            //   ProductoNegocios = new List<ProductoNegocio>();
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Sox { get; set; }
        public bool? EsPlataforma { get; set; }
        public int? IdPlataforma { get; set; }
        [MaxLength(1)]
        public string Modo{ get; set; }

        public bool? AccesoInternet { get; set; }
        public int? IdTipo { get; set; }

        public int? IdServicio { get; set; }

        public int? IdSitio { get; set; }

        public int? IdObsolescencia { get; set; }
      
        public int? IdCicloAplicativo { get; set; }
        public string Gerencia { get; set; }
        public string JefaturaTI { get; set; }
        public string RespTI { get; set; }
        public string RespNegocio { get; set; }
        public string RespMesa { get; set; }
        public bool Activo { get; set; }
        
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
        //public string Agrupador { get; set; }

        //public int? NegocioId { get; set; }
        //public int? ProcesoId { get; set; }
        [ForeignKey("IdCicloAplicativo")]
        public virtual CicloAplicativo CicloAplicativo { get; set; }
        [ForeignKey("IdTipo")]
        public virtual TipoProducto TipoProducto { get; set; }
        [ForeignKey("IdSitio")]
        public virtual Sitio Sitio { get; set; }
        [ForeignKey("IdObsolescencia")]
        public virtual Obsolescencia Obsolescencia { get; set; }
        [ForeignKey("IdServicio")]
        public virtual Servicio Servicio { get; set; }


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual List<ProductoAmbiente> ProductoAmbiente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Modulo> Modulos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Proceso> Procesos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Negocio> Negocios { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual List<ProductoNegocio> ProductoNegocios { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual List<Ambiente> Ambientes { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual List<RolAmbiente> RolesAmbientes { get; set; }
    }
}
