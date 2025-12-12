using System.ComponentModel;

namespace Pampa.InSol.Dal.Consultas
{
    public enum TipoInvocacion
    {
        [Description("StoredProcedure")]
        StoredProcedure = 0,
        [Description("Function")]
        Function = 1
    }
}
