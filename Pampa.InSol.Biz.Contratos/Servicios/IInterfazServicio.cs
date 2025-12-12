using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IInterfazServicio : IServicio<Interfaz>
    {
        Interfaz GetInterfazById(int id);

        void Save(InterfazModel entity);

        void Delete(int id);

        void DeleteComentarioRelacionado(int idInterfaz, int idBitacoraInterfaz);
    }
}
