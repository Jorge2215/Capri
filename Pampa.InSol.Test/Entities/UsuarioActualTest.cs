using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pampa.InSol.Common;
using System.Collections.Generic;

namespace Pampa.InSol.Test.Entities
{
    [TestClass]
    public class UsuarioActualTest
    {
        [TestMethod]
        public void HasPermission()
        {
            var usuario = new UsuarioActual(1, "User\\Name", "Marcelo Gallardo", new List<int>() { 1, 2, 3 }, true);
            usuario.HasPermission(2).Should().BeTrue();
        }

        [TestMethod]
        public void NoHasPermission()
        {
            var usuario = new UsuarioActual(1, "User\\Name", "Marcelo Gallardo", new List<int>() { 1, 2, 3 }, true);
            usuario.HasPermission(4).Should().BeFalse();
        }
    }
}
