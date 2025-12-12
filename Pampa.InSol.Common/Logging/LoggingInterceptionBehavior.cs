using Microsoft.Practices.Unity.InterceptionExtension;
using NLog;
using Pampa.InSol.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Pampa.InSol.Common.Logging
{
    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public bool WillExecute
        {
            get { return true; }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Before invoking the method on the original target.
            this.WriteTrace(string.Format("Invocando al método {0} en {1}", input.MethodBase, DateTime.Now.ToLongTimeString()));

            var result = getNext()(input, getNext);

            var context = HttpContext.Current;
            string userInfo = ObtenerInformacionDeUsuario();
            string arguments = ObtenerArgumentos(input);

            if (result.Exception.IsNotNull())
            {
                this.WriteError(string.Format("{0} {1} - Error al invocar al método: {2}{3}. Ocurrió una excepción: {4}", DateTime.Now.ToLongTimeString(), userInfo, input.MethodBase, arguments, result.Exception.Message));
                throw result.Exception;
            }
            else
            {
                this.WriteTrace(string.Format("{0} {1} - La invocación al método: {2}{3} ha devuelto ({4})", DateTime.Now.ToLongTimeString(), userInfo, input.MethodBase, arguments, result.ReturnValue));
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        private static string ObtenerArgumentos(IMethodInvocation input)
        {
            var argumentWriter = new StringBuilder();
            for (var i = 0; i < input.Arguments.Count; i++)
            {
                var argument = input.Arguments[i];
                var argumentInfo = input.Arguments.GetParameterInfo(i);
                argumentWriter.Append(string.Format(@" nombre: ""{0}"", valor: ""{1}"",", argumentInfo.Name, argument.IsNotNull() ? argument.ToString() : string.Empty));
            }

            var arguments = (argumentWriter.Length > 0) ? " con los argumentos: " + argumentWriter.ToString() : string.Empty;
            return arguments;
        }

        private static string ObtenerInformacionDeUsuario()
        {
            return (HttpContext.Current.IsNull() || HttpContext.Current.User.IsNull() || HttpContext.Current.User.Identity.IsNull()) ? string.Empty : (" - Usuario: " + HttpContext.Current.User.Identity.Name.ToString());
        }

        private void WriteTrace(string message)
        {
            logger.ConditionalTrace(message);
        }

        private void WriteError(string message)
        {
            logger.Error(message);
        }
    }
}
