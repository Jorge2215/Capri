using System.Linq;
using System.Data.Entity;
using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities.Entities;

namespace Pampa.InSol.Biz.Servicios
{
    public class RolAmbienteServicio : AbstractServicio<RolAmbiente>, IRolAmbienteServicio
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RolAmbienteServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public RolAmbiente GetRolAmbienteById(int id)
        {
            var rolAmbiente = this.UnitOfWork.Repository<RolAmbiente>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return rolAmbiente;
        }
    }
}