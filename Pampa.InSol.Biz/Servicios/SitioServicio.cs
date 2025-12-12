using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities.Entities;
using System.Linq;
using System.Data.Entity;

namespace Pampa.InSol.Biz.Servicios
{
    public class SitioServicio : AbstractServicio<Sitio>, ISitioServicio
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public SitioServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public Sitio GetCicloAplicativoById(int id)
        {
            var sitio = this.UnitOfWork.Repository<Sitio>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return sitio;
        }
    }
}
