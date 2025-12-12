using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Biz.Servicios
{
    public class AmbienteServicio : AbstractServicio<Ambiente>, IAmbienteServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AmbienteServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        
        public ProductoAmbiente GetProductoAmbienteByIdAmbiente(int id)
        {
            throw new NotImplementedException();
        }

        public ProductoAmbiente GetProductoAmbienteByIdProducto(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductoAmbiente> GetProductoAmbientesByIds(int idProducto, int idAmbiente)
        {
            throw new NotImplementedException();
        }

        public void SaveProductoAmbiente(ProductoAmbiente entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductoAmbiente(int id)
        {
            throw new NotImplementedException();
        }
    }
}
