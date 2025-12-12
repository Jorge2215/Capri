using Pampa.InSol.Dal.Contratos.Soporte;

namespace Pampa.InSol.Biz.Servicios
{
    public abstract class BaseServicio
    {
        private readonly IUnitOfWork unitOfWork;

        public BaseServicio(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork
        {
            get
            {
                return this.unitOfWork;
            }
        }
    }
}
