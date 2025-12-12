using Microsoft.Practices.Unity.InterceptionExtension;
using Pampa.InSol.Common.Extensions;
using Pampa.InSol.Dal.Contratos.Soporte;

namespace Pampa.InSol.Biz.Servicios.Transaccion
{
    public sealed class TransactionHandler : ICallHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public TransactionHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int Order { get; set; }

        IMethodReturn ICallHandler.Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            // TODO: Parche, verificar por qué el interceptor ingresa dos veces.
            var transaccion = this.unitOfWork.ObtenerTransaccionActiva();
            if (transaccion.IsNull())
            {
                transaccion = this.unitOfWork.IniciarTransacion();

                IMethodReturn result = getNext().Invoke(input, getNext);

                if (result.Exception != null)
                {
                    transaccion.Rollback();
                }
                else
                {
                    if (transaccion.IsNotNull())
                    {
                        transaccion.Commit();
                    }
                }

                return result;
            }
            else
            { 
                return getNext().Invoke(input, getNext);
            }
        }
    }
}