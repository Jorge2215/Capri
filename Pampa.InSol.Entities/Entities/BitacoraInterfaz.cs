using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Entities
{
    [Table("BitacoraInterfaz")]
    public class BitacoraInterfaz : BaseEntity
    {
        public int InterfazId { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }

        public virtual Interfaz Interfaz { get; set; }
    }
}
