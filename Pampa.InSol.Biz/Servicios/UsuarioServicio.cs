using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Biz.Recursos;
using Pampa.InSol.Common;
using Pampa.InSol.Common.Extensions;
using Pampa.InSol.Common.Utils;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Enums;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Hosting;

namespace Pampa.InSol.Biz.Servicios
{
    public class UsuarioServicio : AbstractServicio<Usuario>, IUsuarioServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IServicio<Rol> rolServicio;

        public UsuarioServicio(IUnitOfWork unitOfWork, IRolServicio rolServicio)
            : base(unitOfWork)
        {
            this.rolServicio = rolServicio;
        }

        public virtual bool EsAdministrador(string idRed)
        {
            var usuario = this.UnitOfWork.Repository<Usuario>().Queryable().FirstOrDefault(x => x.UsuarioNT == idRed);
            return usuario.IsNotNull();
        }

        public virtual Usuario GetUsuarioPorId(int id)
        {
            var usuario = this.UnitOfWork.Repository<Usuario>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return usuario;
        }

        public virtual Usuario GetUsuarioPorIdRed(string idRed)
        {
            var usuario = this.UnitOfWork.Repository<Usuario>().Queryable().Where(u => u.UsuarioNT == idRed).AsNoTracking().FirstOrDefault();
            if (usuario.IsNull())
            {
                throw new NullReferenceException(string.Format(BusinessValidationMessages.UsuarioInexistente, idRed));
            }

            return usuario;
        }

        public virtual int ObtenerIdUsuarioContextoActual()
        {
            if (HttpContext.Current.IsNotNull() && HttpContext.Current.User.IsNotNull() && HttpContext.Current.User.Identity.IsNotNull())
            {
                return this.GetUsuarioPorIdRed(HttpContext.Current.User.Identity.Name).Id;
            }

            return -1;
        }

        public virtual IQueryable<UsuarioGrilla> GetUsuariosParaGrilla()
        {
            var usuarios = this.UnitOfWork.Repository<Usuario>()
                .Queryable()
                .Select(x => new UsuarioGrilla
                {
                    Activo = x.Activo,
                    Apellido = x.Apellido,
                    Id = x.Id,
                    Nombre = x.Nombre,
                    UsuarioNT = x.UsuarioNT
                });

            return usuarios;
        }

        public void ActualizarUsuario(Usuario user, IEnumerable<string> listaDeRoles, string usuarioActual)
        {
            logger.Trace("Actualizacion del usuario con ID '{0}'", user.Id);
            using (var transaction = this.UnitOfWork.IniciarTransacion())
            {
                try
                {
                    var usr = this.UnitOfWork.Repository<Usuario>().Queryable().Where(u => u.Id == user.Id).FirstOrDefault();
                    usr.Activo = user.Activo;
                    usr.Apellido = user.Apellido;
                    usr.FechaModificacion = DateTime.Now;
                    usr.ModificadoPor = usuarioActual;
                    usr.Nombre = user.Nombre;

                    // TODO: Evaluar si se puede hacer esto....
                    usr.UsuarioNT = user.UsuarioNT;

                    this.UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.Error(ex, "Al querer actualizar el usuario '{0}'", user.Id);
                    throw;
                }
            }
        }

        public void CreateUsuario(Usuario usuario, IEnumerable<string> listaDeRoles, string usuarioActual)
        {
            logger.Trace("Creación del usuario con Nombre y Apellido '{0}' '{1}'", usuario.Nombre, usuario.Apellido);
            using (var transaction = this.UnitOfWork.IniciarTransacion())
            {
                try
                {
                    usuario.FechaModificacion = DateTime.Now;
                    usuario.ModificadoPor = usuario.CreadoPor = usuarioActual;
                    var usuarioRepo = this.UnitOfWork.Repository<Usuario>();
                    usuarioRepo.Create(usuario);
                    this.UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.Error(ex, "Al querer crear el usuario '{0}'", usuario.Id);
                    throw;
                }
            }
        }

        public void DisableEnableUser(int id)
        {
            logger.Trace("Habilitar / deshabilitar el usuario con ID '{0}'", id);
            using (var transaction = this.UnitOfWork.IniciarTransacion())
            {
                try
                {
                    var usuario = this.UnitOfWork.Repository<Usuario>().Queryable().Where(u => u.Id == id).FirstOrDefault();
                    usuario.Activo = usuario.Activo;
                    usuario.FechaModificacion = System.DateTime.Now;
                    this.UnitOfWork.Repository<Usuario>().Update(usuario);
                    this.UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.Error(ex, "Al querer habilitar/deshabilitar el usuario '{0}'", id);
                    throw;
                }
            }
        }

        public UsuarioActual GetUsuarioActualPorIdRed(string usuarioNT)
        {
            try
            {
                var usuario = this.GetUsuarioPorIdRed(usuarioNT);
                if (usuario.IsNull())
                {
                    return null;
                }

                var usuarioActual = new UsuarioActual(
                                                    usuario.Id,
                                                    usuario.UsuarioNT,
                                                    string.Format("{0} {1}", usuario.Nombre, usuario.Apellido),
                                                    this.GetFunciones(usuario),
                                                    usuario.Activo);

                return usuarioActual;
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
                return null;
            }
        }

        /// <summary>
        /// Devuelve todas las funciones que tiene asignadas un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private List<int> GetFunciones(Usuario usuario)
        {
            var funciones = new List<int>();
            return funciones;
        }

        public UsuarioResumido GetUsuarioAD(string usuarioNTId)
        {
            var usuarioResumido = new UsuarioResumido();
            using (HostingEnvironment.Impersonate())
            {
                DirectoryEntry domain = new DirectoryEntry(string.Concat("LDAP://PAM"));
                DirectorySearcher adsearcher = new DirectorySearcher(domain);
                adsearcher.SearchScope = System.DirectoryServices.SearchScope.Subtree;
                adsearcher.Filter = "(&(objectClass=user)(samaccountname=" + usuarioNTId + "))";
                SearchResult aduser = adsearcher.FindOne();
                if (aduser.IsNull())
                {
                    return null;
                }

                usuarioResumido.Apellido = aduser.Properties["sn"][0].ToString();
                usuarioResumido.Nombre = aduser.Properties["givenname"][0].ToString();
            }

            return usuarioResumido;
        }

        public string ValidarUsuarioExpirado(string username, string password)
        {
            try
            {
                LdapConnection connection = new LdapConnection("PAM.DNS");
                NetworkCredential credential = new NetworkCredential(username.ToUpper(), password);
                connection.Credential = credential;
                connection.Bind();
                return "OK";
            }
            catch (LdapException lexc)
            {
                var codError = lexc.ServerErrorMessage.Split(',')[2].Replace("data ", "").Trim();
                string mensaje = "";
                CodigoErrorEnum.errores.TryGetValue(codError, out mensaje);
                return mensaje != "" ? mensaje : lexc.Message;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return exc.Message;
            }
        }

        public Usuario Insert(UsuarioModel newUser, List<int> rolesId)
        {
            Usuario user = new Usuario(); ;

            //Asegurar que el dominio este en el nombre de red
            string usuarioNT = string.Empty;
            string dominio = string.Format("{0}\\", AppSettings.AppSettingToString("Dominio"));
            if (newUser.UsuarioNT.StartsWith(dominio))
            {
                user.UsuarioNT = newUser.UsuarioNT.ToUpper();
            }
            else
            {
                user.UsuarioNT = string.Format("{0}{1}", dominio, newUser.UsuarioNT.ToUpper());
            }

            //valido que el usuario de Red no exista
            Usuario userExistente = this.GetAll(u => u.UsuarioNT.Equals(user.UsuarioNT) && u.Id != newUser.Id).FirstOrDefault();

            if (userExistente != default(Usuario))
            {
                throw new Exception("El Usuario de Red que se desea agregar ya existe para otro Usuario");
            }

            if (newUser.Id != 0)
            {
                user = this.GetOne(newUser.Id);

                if (user == default(Usuario))
                {
                    throw new Exception("El Usuario que se desea editar no existe");
                }
            }

            user.Nombre = newUser.Nombre;
            user.Apellido = newUser.Apellido;
            user.Activo = newUser.Activo;

            IEnumerable<Rol> rolesEliminar = user.Roles.Where(r => (rolesId == null || !rolesId.Contains(r.Id))).ToList();

            foreach (Rol rolEliminar in rolesEliminar)
            {
                user.Roles.Remove(rolEliminar);
            }

            if (rolesId != null && rolesId.Any())
            {
                //Agregar o Sacar Roles
                foreach (int rolId in rolesId)
                {
                    if (!user.Roles.Any(r => r.Id == rolId))
                    {
                        Rol newRol = rolServicio.GetOne(rolId);
                        if (newRol == default(Rol))
                        {
                            throw new Exception(string.Format("El Rol {0} no existe", rolId));
                        }

                        user.Roles.Add(newRol);
                    }
                }
            }
            if (newUser.Id == 0)
            {
                user.CreadoPor = Seguridad.Security.UserNT;
                user.FechaCreacion = DateTime.Now;
                user.FechaModificacion = DateTime.Now;
                user.ModificadoPor = Seguridad.Security.UserNT;
                user = InsertAndSave(user);
            }
            else
            {
                user.ModificadoPor = Seguridad.Security.UserNT;
                user.FechaModificacion = DateTime.Now;
                user = UpdateAndSave(user);
            }
            return user;
        }

        public UsuarioResumido GetUsuarioADbyUPN(string userPrincipalName)
        {
            var usuarioResumido = new UsuarioResumido();
            using (HostingEnvironment.Impersonate())
            {
                DirectoryEntry domain = new DirectoryEntry(string.Concat("LDAP://PAM"));
                DirectorySearcher adsearcher = new DirectorySearcher(domain);
                adsearcher.SearchScope = System.DirectoryServices.SearchScope.Subtree;
                adsearcher.Filter = $"(&(objectClass=user)(userprincipalname={userPrincipalName}*))";
                SearchResult aduser = adsearcher.FindOne();
                if (aduser.IsNull())
                {
                    return null;
                }

                usuarioResumido.Apellido = aduser.Properties["sn"][0].ToString();
                usuarioResumido.Nombre = aduser.Properties["givenname"][0].ToString();
                usuarioResumido.UsuarioNT = aduser.Properties["cn"][0].ToString();
            }

            return usuarioResumido;
        }
    }
}