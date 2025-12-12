namespace Pampa.InSol.Entities.Models
{
    public class ProductoNegocioModel
    {
        public int IdProducto { get; set; }
        public int IdNegocio { get; set; }
        public string Descripcion { get; set; }

        public string ProductoDescripcion { get; set; }
        public string NegocioDescripcion { get; set; }
    }
}