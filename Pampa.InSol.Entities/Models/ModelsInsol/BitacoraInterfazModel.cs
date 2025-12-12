using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Entities.Models
{
    public class BitacoraInterfazModel
    {
        public int Id { get; set; }
        public int InterfazId { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }
    }
}
