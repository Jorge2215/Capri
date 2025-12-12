using Moq;
using Pampa.InSol.Biz.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;

namespace Pampa.InSol.Test.Builders.Servicios
{
    internal class RolServicioBuilder
    {
        private Mock<RolServicio> servicio;

        public RolServicioBuilder(IUnitOfWork unitOfWork)
        {
            this.servicio = new Mock<RolServicio>(unitOfWork);
        }

        public RolServicio Build()
        {
            return this.servicio.Object;
        }

        public Mock<RolServicio> BuildMock()
        {
            return this.servicio;
        }
    }
}
