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
    public class ObsolescenciaServicio : AbstractServicio<Obsolescencia>, IObsolescenciaServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ObsolescenciaServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public Obsolescencia GetCicloAplicativoById(int id)
        {
            var obsolescencia = this.UnitOfWork.Repository<Obsolescencia>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return obsolescencia;
        }
    }
}
