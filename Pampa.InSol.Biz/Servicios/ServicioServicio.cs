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
    public class ServicioServicio : AbstractServicio<Servicio>, IServicioServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ServicioServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public Servicio GetCicloAplicativoById(int id)
        {
            var servicio = this.UnitOfWork.Repository<Servicio>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return servicio;
        }

    }
}
