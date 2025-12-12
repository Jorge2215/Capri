using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Models
{
    public class ProductoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdPlataforma { get; set; }
        public int? IdCicloAplicativo { get; set; }
        public string RespNegocio { get; set; }
        public string RespMesa { get; set; }
        public string RespTI { get; set; }
        public bool Activo { get; set; }
        public string Gerencia { get; set; }
        public string JefaturaTI { get; set; }
        public bool Sox { get; set; }
        public int? IdObsolescencia { get; set; }
        public string Agrupador { get; set; }
        public int? IdTipo { get; set; }
        public int? IdSitio { get; set; }
        public int? IdServicio { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }

        public bool EsPlataforma { get; set; }

        public string Modo { get; set; }

        public bool AccesoInternet { get; set; }
        //public int? NegocioId { get; set; }
        //public int? ProcesoId { get; set; }

        public CicloAplicativoModel CicloAplicativo { get; set; }
        public TipoProductoModel TipoProducto { get; set; }

        public AmbienteModel Ambiente { get; set; }
        //public ProductoAmbienteModel AmbienteDEV { get; set; }
        //public ProductoAmbienteModel AmbienteQA { get; set; }
        //public ProductoAmbienteModel AmbientePROD { get; set; }

        public List<ProductoModuloModel> ListaModulos { get; set; }

        public List<ProductoProcesoModel> ListaProcesos { get; set; }
        public List<ProductoNegocioModel> ListaNegocios { get; set; }

        public List<NegocioModel> Negocios { get; set; }

        public List<ProcesoModel> Procesos { get; set; }

        //public List<AmbienteModel> Ambientes { get; set; }

        public ProductoAmbienteModel ProductoAmbiente { get; set; }

        public string CicloAplicativoDisplay
        {
            get => (CicloAplicativo != null) ? CicloAplicativo.Descripcion : string.Empty;
        }
        public string TipoProductoDisplay
        {
            get => (TipoProducto != null) ? TipoProducto.Descripcion : string.Empty;
        }
        //public bool EsPlataformaDisplay { get => (EsPlataforma == null) ? EsPlataforma = false ? EsPlataforma; }
        public string NegociosDisplay
        {
            get
            {
                string aux = string.Empty;
                if(Negocios.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in Negocios)
                    {
                        sb.Append(item.Descripcion);
                        sb.Append(" - ");
                    }
                    sb.Remove(sb.Length - 3, 3);
                    aux = sb.ToString();
                }
                return aux;
            }
        }
        public string ProcesosDisplay
        {
            get
            {
                string aux = string.Empty;
                if (Procesos.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in Procesos)
                    {
                        sb.Append(item.Descripcion);
                        sb.Append(" - ");
                    }
                    sb.Remove(sb.Length - 3, 3);
                    aux = sb.ToString();
                }
                return aux;
            }
        }

    }
}
