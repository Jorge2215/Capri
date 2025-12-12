using Pampa.InSol.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;
namespace Pampa.InSol.Biz.Servicios
{
    public class TipoProductoServicio : AbstractServicio<TipoProducto>, ITipoProductoServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TipoProductoServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public TipoProducto GetCicloAplicativoById(int id)
        {
            var tipoProducto = this.UnitOfWork.Repository<TipoProducto>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return tipoProducto;
        }
    }
}
