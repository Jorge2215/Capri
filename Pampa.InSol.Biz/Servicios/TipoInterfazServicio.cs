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
    public class TipoInterfazServicio : AbstractServicio<TipoInterfaz>, ITipoInterfazServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TipoInterfazServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
