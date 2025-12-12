using System.ComponentModel.DataAnnotations.Schema;

namespace Pampa.InSol.Entities.Entities
{
    [Table("RolAmbiente")]
    public class RolAmbiente : BaseEntity
    {
        public string Descripcion { get; set; }
    }
}
