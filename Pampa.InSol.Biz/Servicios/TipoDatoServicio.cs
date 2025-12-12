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
    public class TipoDatoServicio : AbstractServicio<TipoDato>, ITipoDatoServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TipoDatoServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
