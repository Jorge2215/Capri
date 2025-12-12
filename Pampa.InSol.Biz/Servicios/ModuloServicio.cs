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
    public class ModuloServicio : AbstractServicio<Modulo>, IModuloServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ModuloServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public bool ValidateDuplicated(int? id, string desc)
        {
            return !GetAll().Any(r => r.Nombre.Equals(desc, StringComparison.InvariantCultureIgnoreCase) && (!id.HasValue || id.Value != r.Id));
        }
    }
}
