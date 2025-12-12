using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Pampa.InSol.Biz.Seguridad;

namespace Pampa.InSol.Biz.Servicios
{
    public class ProductoServicio : AbstractServicio<Producto>, IProductoServicio
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IModuloServicio moduloServicio;
        private readonly IInterfazServicio interfazServicio;
        private readonly IServicio<Entities.Entities.Negocio> negocioServicio;
        private readonly IServicio<Proceso> procesoServicio;

        public ProductoServicio(IUnitOfWork unitOfWork, IModuloServicio moduloServicio, IInterfazServicio interfazServicio, IServicio<Entities.Entities.Negocio> negocioServicio, IServicio<Proceso> procesoServicio) : base(unitOfWork)
        {
            this.moduloServicio = moduloServicio;
            this.interfazServicio = interfazServicio;
            this.negocioServicio = negocioServicio;
            this.procesoServicio = procesoServicio;
        }


        public Producto GetProductoById(int id)
        {
            var producto = this.UnitOfWork.Repository<Producto>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return producto;
        }

        public Producto Save(ProductoModel entity)
        {
            Producto producto = new Producto();

            if (!ValidateDuplicated(entity.Id, entity.Nombre))
                throw new Exception("Ya existe un Producto con el nombre");

            if (entity.Id != 0)
            {
                producto = GetOne(entity.Id);
                if (entity.Activo == false && producto.Activo == true)
                {
                    if (interfazServicio.GetAll(x => x.IdProductoOrigen == producto.Id || x.IdProductoDestino == producto.Id).Where(i => i.Activo == true).Count() > 0)
                        throw new Exception("No se puede eliminar el producto, </br>existen Interfaces asociadas al mismo.");
                }
            }

            producto.Nombre = entity.Nombre;
            producto.Descripcion = entity.Descripcion;
            producto.EsPlataforma = entity.EsPlataforma;
            producto.IdPlataforma = entity.IdPlataforma;
            producto.Modo = entity.Modo;
            producto.IdCicloAplicativo = entity.IdCicloAplicativo == 0 ? null : entity.IdCicloAplicativo;
            producto.IdObsolescencia = entity.IdObsolescencia == 0 ? null : entity.IdObsolescencia;
            producto.IdTipo = entity.IdTipo == 0 ? null : entity.IdTipo;
            producto.IdSitio = entity.IdSitio == 0 ? null : entity.IdSitio;
            producto.IdServicio = entity.IdServicio == 0 ? null : entity.IdServicio;
            producto.RespNegocio = entity.RespNegocio;
            producto.RespMesa = entity.RespMesa;
            producto.RespTI = entity.RespTI;
            producto.Activo = entity.Activo;
            producto.Gerencia = entity.Gerencia;
            producto.JefaturaTI = entity.JefaturaTI;
            producto.Sox = entity.Sox;
            producto.AccesoInternet = entity.AccesoInternet;

            #region Modulos
            if (entity.ListaModulos != null)
            {
                foreach (var moduloModel in entity.ListaModulos)
                {
                    if (moduloModel.IdModulo == 0)
                    {
                        if (!moduloServicio.ValidateDuplicated(moduloModel.IdModulo, moduloModel.Nombre))
                            throw new Exception($"Ya existe un Módulo con el nombre {moduloModel.Nombre}");
                        else
                        {
                            var nuevoModulo = new Modulo();
                            nuevoModulo.Nombre = moduloModel.Nombre;
                            nuevoModulo.Descripcion = moduloModel.Descripcion;
                            producto.Modulos.Add(nuevoModulo);
                        }
                    }
                }
            }
            #endregion

            #region Negocios

            if (entity.ListaNegocios != null)
            {
                List<Entities.Entities.Negocio> negociosActualizado = new List<Entities.Entities.Negocio>();
                foreach (var item in entity.ListaNegocios)
                {
                    negociosActualizado.Add(this.negocioServicio.GetOne(item.IdNegocio));
                }
                List<Entities.Entities.Negocio> negociosAEliminar = new List<Entities.Entities.Negocio>();
                foreach (var item in producto.Negocios)
                {
                    if (!negociosActualizado.Contains(this.negocioServicio.GetOne(item.Id)))
                        negociosAEliminar.Add(item);

                }
                foreach (var item in negociosAEliminar)
                {
                    producto.Negocios.Remove(this.negocioServicio.GetOne(item.Id));
                }
                producto.Negocios = negociosActualizado;
            }
            else
            {
                if (producto.Negocios != null)
                {
                    List<Entities.Entities.Negocio> negociosAEliminar = new List<Entities.Entities.Negocio>();
                    foreach (var item in producto.Negocios)
                    {
                        negociosAEliminar.Add(item);
                    }
                    foreach (var item in negociosAEliminar)
                    {
                        producto.Negocios.Remove(this.negocioServicio.GetOne(item.Id));
                    }
                }
            }

            #endregion

            #region Procesos
            if (entity.ListaProcesos != null)
            {
                List<Entities.Entities.Proceso> procesosActualizado = new List<Entities.Entities.Proceso>();
                foreach (var item in entity.ListaProcesos)
                {
                    procesosActualizado.Add(this.procesoServicio.GetOne(item.IdProceso));
                }
                List<Entities.Entities.Proceso> procesosAEliminar = new List<Entities.Entities.Proceso>();
                foreach (var item in producto.Procesos)
                {
                    if (!procesosActualizado.Contains(this.procesoServicio.GetOne(item.Id)))
                        procesosAEliminar.Add(item);

                }
                foreach (var item in procesosAEliminar)
                {
                    producto.Procesos.Remove(this.procesoServicio.GetOne(item.Id));
                }
                producto.Procesos = procesosActualizado;
            }
            else
            {
                if (producto.Procesos != null)
                {
                    List<Entities.Entities.Proceso> procesosAEliminar = new List<Entities.Entities.Proceso>();
                    foreach (var item in producto.Procesos)
                    {
                        procesosAEliminar.Add(item);
                    }
                    foreach (var item in procesosAEliminar)
                    {
                        producto.Procesos.Remove(this.procesoServicio.GetOne(item.Id));
                    }
                }
            }
            #endregion

            if (entity.Id == 0)
            {
                producto.CreadoPor = Security.FullNameCurrentUser;
                producto.ModificadoPor = string.Empty;
                producto.FechaCreacion = DateTime.Now;
                producto.FechaModificacion = DateTime.Now;
                return InsertAndSave(producto);
            }
            else
            {
                producto.FechaModificacion = DateTime.Now;
                producto.ModificadoPor = Security.FullNameCurrentUser;
                return UpdateAndSave(producto);
            }
        }

        public bool ValidateDuplicated(int? id, string desc)
        {
            return !GetAll().Any(r => r.Nombre.Equals(desc, StringComparison.InvariantCultureIgnoreCase) && (!id.HasValue || id.Value != r.Id));
        }

        public bool Delete(int id)
        {
            var interfaces = interfazServicio.GetAll(x => x.IdProductoOrigen == id || x.IdProductoDestino == id).Where(i => i.Activo == true);

            if (interfaces != null && interfaces.Count() > 0)
            {
                return false;
                //throw new Exception("No se puede eliminar el producto, ya que existen una o más Interfaces asociadas al mismo");
            }
            else
            {
                var producto = GetProductoById(id);
                producto.Activo = !producto.Activo;
                UpdateAndSave(producto);

                return true;
            }
        }

        public void DeleteModuloRelacionado(int idProducto, string nombre)
        {
            var producto = GetOne(idProducto);
            if (producto.Modulos.Count > 0)
            {
                var modulo = producto.Modulos.Find(x => x.Nombre == nombre);
                if (modulo != null)
                {
                    producto.Modulos.Remove(modulo);

                    producto.FechaModificacion = DateTime.Now;
                    producto.ModificadoPor = Security.FullNameCurrentUser;
                    UpdateAndSave(producto);
                }
            }
        }

        private void FillProductoAmbiente(ProductoAmbiente ambiente, ProductoAmbienteModel ambienteModel)
        {
            //ambiente.IdAmbiente = ambienteModel.IdAmbiente;
            //ambiente.AppPath = ambienteModel.AppPath;
            //ambiente.AppURL = ambienteModel.AppURL;
            //ambiente.BaseServer = ambienteModel.BaseServer;
            //ambiente.BaseNombre = ambienteModel.BaseNombre;
            //ambiente.BaseUsuario = ambienteModel.BaseUsuario;
            //ambiente.BasePass = ambienteModel.BasePass;
            //ambiente.UsuarioEmer = ambienteModel.UsuEmer;
        }
    }
}
