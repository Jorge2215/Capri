namespace Pampa.InSol.Biz.Servicios
{
    using Pampa.InSol.Dal.Contratos.Soporte;
    using System.Linq;

    public class NegocioServicio : AbstractServicio<Entities.Entities.Negocio>
    {
        public NegocioServicio(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    }
}
