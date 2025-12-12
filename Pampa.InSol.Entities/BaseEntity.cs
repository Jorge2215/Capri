using System.ComponentModel.DataAnnotations;

namespace Pampa.InSol.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
