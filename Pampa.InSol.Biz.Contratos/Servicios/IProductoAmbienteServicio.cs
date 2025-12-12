using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Entities.Models;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IProductoAmbienteServicio : IServicio<ProductoAmbiente>
    {
        ProductoAmbiente GetProductoAmbienteByIdProducto(int id);

        void Save(ProductoAmbienteModel entity);

        void Delete(int id);

        void Delete(ProductoAmbiente id);

        //void DeleteComentarioRelacionado(int idInterfaz, int idBitacoraInterfaz);
    }
}
