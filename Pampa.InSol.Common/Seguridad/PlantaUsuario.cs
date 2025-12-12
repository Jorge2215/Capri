namespace Pampa.InSol.Common
{
    public class PlantaUsuario
    {
        public decimal Id { get; set; }

        public string Nombre { get; set; }

        public string NombreCompleto
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Id, this.Nombre);
        }
    }
}
