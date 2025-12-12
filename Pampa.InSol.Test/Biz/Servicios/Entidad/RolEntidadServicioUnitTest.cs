using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pampa.InSol.Biz.Servicios;
using Pampa.InSol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pampa.InSol.Test
{
    [TestClass]
    public class RolEntidadServicioUnitTest
    {
        [TestMethod]
        public void GetDescripcionesDevuelveTodosLasDescripcionesDeLosRoles()
        {
            // Arrange
            const int CantidadDeRegistro = 10;
            var dataRoles = this.CrearListaDeRoles(CantidadDeRegistro).AsQueryable();
            var uow = new UoWBuilder().WithData<Rol>(dataRoles).Build();
            var service = new RolServicio(uow);
            var roles = service.GetDescripciones();
            roles.Count().Should().Be(5);
        }

        /// <summary>
        /// Crea una lista con la cantidad de entidades solicitadas donde la mitad estaran activas
        /// </summary>
        private IEnumerable<Rol> CrearListaDeRoles(int cantidad)
        {
            int id = 0;
            var roles = new List<Rol>();
            for (var row = 0; row < cantidad; row++)
            {
                id++;
                roles.Add(new Rol
                {
                    Id = id,
                    Activo = id % 2 == 1,
                    CreadoPor = string.Format("Usuario creador {0}", id),
                    Descripcion = string.Format("Descripcion registro {0}", id),
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Funciones = null, // para el ejemplo no se requiere
                    ModificadoPor = string.Format("Usuario creador {0}", id),
                    Usuarios = null // para el ejemplo no se requiere
                });
            }

            return roles;
        }
    }
}
