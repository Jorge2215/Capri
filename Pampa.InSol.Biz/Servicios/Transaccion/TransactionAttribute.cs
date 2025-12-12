using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Pampa.InSol.Dal.Contratos.Soporte;
using System;

namespace Pampa.InSol.Biz.Servicios.Transaccion
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TransactionAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new TransactionHandler(container.Resolve<IUnitOfWork>());
        }
    }
}
