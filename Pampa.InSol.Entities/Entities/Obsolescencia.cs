using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pampa.InSol.Entities.Entities
{
    [Table("Obsolescencia")]
    public class Obsolescencia : BaseEntity
    {
        public string Descripcion { get; set; }
    }
}
