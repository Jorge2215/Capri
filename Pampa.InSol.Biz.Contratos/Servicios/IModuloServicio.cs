using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pampa.InSol.Entities.Entities;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IModuloServicio : IServicio<Modulo>
    {
        bool ValidateDuplicated(int? id, string desc);
    }
}
