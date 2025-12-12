using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pampa.InSol.Biz.Servicios;
using Pampa.InSol.Biz.Servicios.Negocio;
using Pampa.InSol.Entities;
using Pampa.InSol.Test.Builders.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pampa.InSol.Test.Biz.Servicios.Negocio
{
    [TestClass]
    public class UsuarioServicioTest
    {
        //[TestMethod]
        //public void DisableEnableUserTest()
        //{
        //    var usuario = new Usuario() { Id = 1, Activo = false };
        //    var usuarios = new List<Usuario>() { usuario };
        //    var uow = new UoWBuilder().WithData<Usuario>(usuarios.AsQueryable()).Build();
        //    var mockRolEntidadServicio = new RolServicioBuilder(uow).BuildMock();
        //    var servicio = new UsuarioServicio(uow);
        //    servicio.DisableEnableUser(1);
        //    usuario.Activo.Should().Be(true);
        //}

        //[TestMethod]
        //public void ActualizarUsuarioTest()
        //{
        //    var usuario = new Usuario() { Id = 1, Activo = false, Nombre = "Eric" };
        //    var usuarios = new List<Usuario>() { usuario };
        //    var uow = new UoWBuilder().WithData<Usuario>(usuarios.AsQueryable()).Build();
        //    var mockRolEntidadServicio = new RolServicioBuilder(uow).BuildMock();
        //    var servicio = new UsuarioServicio(uow);
        //    var datosActualizacion = new Usuario() { Id = 1, Activo = true, Nombre = "Santiago" };
        //    servicio.ActualizarUsuario(datosActualizacion, new List<string>(), "Eric");
        //    this.UsuarioActualizaCorrectamente(usuario);
        //}

        //[TestMethod]
        //public void UsuarioPorIdRedDevuelveUsuarioActualConSusPermisos()
        //{
        //    const string UsuarioNT = "PETRO\\D7QO";
        //    var funciones = new List<Funcion>() { new Funcion() { Id = 1 }, new Funcion() { Id = 2 } };
        //    var roles = new List<Rol>() { new Rol() { Id = 1, Funciones = funciones } };
        //    var usuario = new Usuario() { Id = 1, UsuarioNT = UsuarioNT, Roles = roles };
        //    var usuarios = new List<Usuario>() { usuario };
        //    var uow = new UoWBuilder().WithData<Usuario>(usuarios.AsQueryable()).Build();
        //    var mockRolEntidadServicio = new RolServicioBuilder(uow).BuildMock();
        //    var servicio = new UsuarioServicio(uow);
        //    var usuarioActual = servicio.GetUsuarioActualPorIdRed(UsuarioNT);
        //    usuarioActual.UserFunctionality.Count().Should().Be(2);
        //}

        //[TestMethod]
        //public void UsuarioPorIdRedQueNoExiste()
        //{
        //    const string UsuarioNT = "PETRO\\D7QO";
        //    const string Usuario2NT = "PETRO\\D8QO";
        //    var usuarios = new List<Usuario>() { new Usuario() { UsuarioNT = UsuarioNT } };
        //    var uow = new UoWBuilder().WithData<Usuario>(usuarios.AsQueryable()).Build();
        //    var mockRolEntidadServicio = new RolServicioBuilder(uow).BuildMock();
        //    var servicio = new UsuarioServicio(uow);
        //    var usuarioActual = servicio.GetUsuarioActualPorIdRed(Usuario2NT);
        //    usuarioActual.Should().BeNull();
        //}

        //private void UsuarioActualizaCorrectamente(Usuario usuario)
        //{
        //    usuario.Nombre.Should().Be("Santiago");
        //    usuario.Activo.Should().Be(true);
        //}
    }
}
