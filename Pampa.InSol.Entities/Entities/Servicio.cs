using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Entities
{
    [Table("Servicio")]
    public class Servicio : BaseEntity
    {
        public string Descripcion { get; set; }
    }
}
