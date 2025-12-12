////using Moq;
////using Pampa.InSol.Biz.Servicios;
////using Pampa.InSol.Dal.Contratos.Soporte;

////namespace Pampa.InSol.Test.Builders.Servicios
////{
////    internal class UsuarioServicioBuilder : IBuilder<UsuarioServicio>
////    {
////        private IUnitOfWork unitOfWork = new Mock<IUnitOfWork>().Object;

////        public UsuarioServicio Build()
////        {
////            var servicio = new UsuarioServicio(this.unitOfWork);
////            return servicio;
////        }

////        public UsuarioServicioBuilder WithUoW(IUnitOfWork unitOfWork)
////        {
////            this.unitOfWork = unitOfWork;
////            return this;
////        }
////    }
////}
