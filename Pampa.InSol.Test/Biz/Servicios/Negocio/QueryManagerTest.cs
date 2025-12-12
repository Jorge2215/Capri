using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pampa.InSol.Dal.Consultas;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Pampa.InSol.Test.Biz.Servicios.Negocio
{
    [TestClass]
    public class QueryManagerTest
    {
        [TestMethod]
        public void GenerarConsultaProcedimientoTest()
        {
            string nombreProcedimiento = "ProcedimientoTest";
            string nombreParam1 = "Param1";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add(nombreParam1, 1);
            var manager = new QueryManager();

            manager.GenerateQuery(TipoInvocacion.StoredProcedure, nombreProcedimiento, parametros);
            manager.Query.Should().Be("exec " + nombreProcedimiento + " @" + nombreParam1);
        }

        [TestMethod]
        public void GenerarConsultaParaFunctionTest()
        {
            string nombreFuncion = "FuncionTest";
            string nombreParam1 = "Param1";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add(nombreParam1, 1);

            var manager = new QueryManager();
            
            manager.GenerateQuery(TipoInvocacion.Function, nombreFuncion, parametros);
            manager.Query.Should().Be("select dbo." + nombreFuncion + " (@" + nombreParam1 + ")");
        }
    }
}
