using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pampa.InSol.Mvc.Controllers;
using System;
using System.Linq;
using System.Reflection;

namespace Pampa.InSol.Test.Arquitectura
{
    [TestClass]
    public class LineamientosTest
    {
        [TestMethod]
        public void TodosLosControladoresHeredanDeBaseController()
        {
            var baseControllerType = typeof(BaseController);
            var controllers = this.GetControllersTypeInNamespace(baseControllerType).ToList();
            controllers.ForEach(x => x.IsSubclassOf(baseControllerType)
                                    .Should()
                                    .BeTrue("El controlador {0} debe heredar de BaseController", x.Name));
        }

        private Type[] GetControllersTypeInNamespace(Type controllerBaseType)
        {
            var assembly = Assembly.GetAssembly(controllerBaseType);
            var nameSpace = controllerBaseType.Namespace;
            var name = controllerBaseType.Name;

            return assembly
                .GetTypes()
                .Where(type =>
                       string.Equals(type.Namespace, nameSpace, StringComparison.Ordinal) &&
                       type.Name != name &&
                       type.Name.EndsWith("Controller"))
                .ToArray();
        }
    }
}
