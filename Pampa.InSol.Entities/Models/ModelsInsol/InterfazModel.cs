using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Models
{
    public class InterfazModel
    {
        public int Id { get; set; }
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

        public string InOut { get; set; }

        public virtual ModuloModel ModuloOrigen { get; set; }
        public virtual ProductoModel ProductoOrigen { get; set; }
        public virtual ProductoModel ProductoDestino { get; set; }
        public virtual ModuloModel ModuloDestino { get; set; }

        public virtual CicloAplicativoModel CicloAplicativo { get; set; }
        public virtual TipoDatoModel TipoDato { get; set; }
        public virtual FrecuenciaModel Frecuencia { get; set; }
        public virtual TipoInterfazModel TipoInterfaz { get; set; }

        public List<BitacoraInterfazModel> ListaBitacora { get; set; }

        public string ModuloOrigenDisplay
        {
            get => (ModuloOrigen != null) ? ModuloOrigen.Nombre : string.Empty;
        }

        public string ProductoOrigenDisplay
        {
            get => (ProductoOrigen != null) ? ProductoOrigen.Nombre : string.Empty;
        }

        public string ProductoDestinoDisplay
        {
            get => (ProductoDestino != null) ? ProductoDestino.Nombre : string.Empty;
        }

        public string ModuloDestinoDisplay
        {
            get => (ModuloDestino != null) ? ModuloDestino.Nombre : string.Empty;
        }

        public string CicloAplicativoDisplay
        {
            get => (CicloAplicativo != null) ? CicloAplicativo.Descripcion : string.Empty;
        }

        public string TipoDatoDisplay
        {
            get => (TipoDato != null) ? TipoDato.Descripcion : string.Empty;
        }

        public string FrecuenciaDisplay
        {
            get => (Frecuencia != null) ? Frecuencia.Descripcion : string.Empty;
        }

        public string TipoInterfazDisplay
        {
            get => (TipoInterfaz != null) ? TipoInterfaz.Descripcion : string.Empty;
        }
    }
}
