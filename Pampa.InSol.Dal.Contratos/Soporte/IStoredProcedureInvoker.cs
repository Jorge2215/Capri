using System.Collections.Generic;

namespace Pampa.InSol.Dal.Contratos.Soporte
{
    public interface IStoredProcedureInvoker
    {
        List<T> InvocarStoreProcedureDinamico<T>(string nombre, Dictionary<string, object> parametros = null);

        T InvocarFuncion<T>(string nombre, Dictionary<string, object> parametros);
    }
}
