using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pampa.InSol.Common.Extensions;
using System;

namespace Pampa.InSol.Test.Common.Extensions
{
    [TestClass]
    public class DatetimeExtesionTest
    {
        [TestMethod]
        public void PrimerDiaDelMes()
        {
            var fecha = new DateTime(2016, 5, 31);
            var fechaEsperada = new DateTime(2016, 5, 1);
            fecha.PrimerDiaDelMes().Should().Be(fechaEsperada);
        }
    }
}