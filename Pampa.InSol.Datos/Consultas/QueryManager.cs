using Pampa.InSol.Common.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace Pampa.InSol.Dal.Consultas
{
    public class QueryManager
    {
        public QueryManager()
        {
            this.Params = new SqlParameter[0];
        }

        public string Query { get; set; }

        public SqlParameter[] Params { get; set; }

        public void GenerateQuery(TipoInvocacion tipoInvocacion, string nombre, Dictionary<string, object> parametros)
        {
            int count = parametros.IsNull() ? 0 : parametros.Keys.Count;

            this.Params = new SqlParameter[count];

            this.Query = this.ObtenerPrefijo(tipoInvocacion) + nombre + " ";
            if (parametros.IsNotNull())
            {
                int i = 0;
                if (this.EntreParentesis(tipoInvocacion))
                {
                    this.Query += "(";
                }

                foreach (var param in parametros)
                {
                    this.Params[i] = new SqlParameter(param.Key, param.Value);
                    this.Query += "@" + param.Key + ",";
                    i++;
                }

                if (parametros.Count > 0)
                {
                    this.Query = this.Query.Substring(0, this.Query.Length - 1);
                    if (this.EntreParentesis(tipoInvocacion))
                    {
                        this.Query += ")";
                    }
                }
            }
        }

        private string ObtenerPrefijo(TipoInvocacion tipoInvocacion)
        {
            switch (tipoInvocacion)     
            {
                case TipoInvocacion.StoredProcedure:
                    return "exec ";
                case TipoInvocacion.Function:
                    return "select dbo.";
                default:
                    throw new InvalidEnumArgumentException("tipoInvocacion", (int)tipoInvocacion, typeof(TipoInvocacion));
            }
        }

        private bool EntreParentesis(TipoInvocacion tipoInvocacion)
        {
            switch (tipoInvocacion)
            {
                case TipoInvocacion.Function:
                    return true;
                default:
                    return false;
            }
        }
    }
}
