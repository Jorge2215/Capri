using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Pampa.InSol.Biz.Servicios
{
    public class CicloAplicativoServicio : AbstractServicio<CicloAplicativo>, ICicloAplicativoServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CicloAplicativoServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public CicloAplicativo GetCicloAplicativoById(int id)
        {
            var cicloAplicativo = this.UnitOfWork.Repository<CicloAplicativo>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return cicloAplicativo;
        }
    }
}
