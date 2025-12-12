namespace Pampa.InSol.Biz.Servicios
{
    using Pampa.InSol.Dal.Contratos.Soporte;
    using Pampa.InSol.Entities.Entities;
    public class ProcesoServicio : AbstractServicio<Proceso>
    {
        public ProcesoServicio(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
