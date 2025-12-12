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
    public class InterfazServicio : AbstractServicio<Interfaz>, IInterfazServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public InterfazServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }


        public Interfaz GetInterfazById(int id)
        {
            var interfaz = this.UnitOfWork.Repository<Interfaz>().Queryable().Where(u => u.Id == id).AsNoTracking().FirstOrDefault();
            return interfaz;
        }

        public void Save(InterfazModel entity)
        {
            Interfaz interfaz = new Interfaz();

            if (!ValidateDuplicated(entity.Id, entity.Nombre))
                throw new Exception("Ya existe una Interfaz con el nombre");

            if (entity.Id != 0)
                interfaz = GetOne(entity.Id);

            interfaz.Nombre = entity.Nombre;
            interfaz.CicloAplicativoId = (entity.CicloAplicativoId == 0) ? null : entity.CicloAplicativoId;
            interfaz.Dato = entity.Dato;
            interfaz.TipoDatoId = (entity.TipoDatoId == 0) ? null : entity.TipoDatoId;
            interfaz.FrecuenciaId = entity.FrecuenciaId;
            interfaz.TipoInterfazId = entity.TipoInterfazId;
            interfaz.IdProductoOrigen = entity.IdProductoOrigen;
            interfaz.IdModuloOrigen = (entity.IdModuloOrigen == 0) ? null : entity.IdModuloOrigen;
            interfaz.IdProductoDestino = entity.IdProductoDestino;
            interfaz.IdModuloDestino = (entity.IdModuloDestino == 0) ? null : entity.IdModuloDestino;
            interfaz.TecnologiaId = entity.TecnologiaId;
            interfaz.TransporteId = entity.TransporteId;
            interfaz.Activo = entity.Activo;

            //Bitacora Interfaz
            if (entity.ListaBitacora != null)
            {
                foreach (var bitacoraModel in entity.ListaBitacora)
                {
                    if (bitacoraModel.Id == 0)
                    {
                        var bitacora = new BitacoraInterfaz();
                        bitacora.Fecha = DateTime.Now;
                        bitacora.IdUsuario = bitacoraModel.IdUsuario;
                        bitacora.Usuario = bitacoraModel.Usuario;
                        bitacora.Comentario = bitacoraModel.Comentario;
                        interfaz.BitacoraInterfaz.Add(bitacora);
                    }
                }
            }

            if (entity.Id == 0)
            {
                interfaz.CreadoPor = Security.FullNameCurrentUser;
                interfaz.ModificadoPor = string.Empty;
                interfaz.FechaCreacion = DateTime.Now;
                interfaz.FechaModificacion = DateTime.Now;
                InsertAndSave(interfaz);
            }
            else
            {
                interfaz.FechaModificacion = DateTime.Now;
                interfaz.ModificadoPor = Security.FullNameCurrentUser;
                UpdateAndSave(interfaz);
            }
        }

        public bool ValidateDuplicated(int? id, string desc)
        {
            return !GetAll().Any(r => r.Nombre.Equals(desc, StringComparison.InvariantCultureIgnoreCase) && (!id.HasValue || id.Value != r.Id));
        }

        public void Delete(int id)
        {
            var interfaz = GetInterfazById(id);
            interfaz.Activo = !interfaz.Activo;
            UpdateAndSave(interfaz);
        }

        public void DeleteComentarioRelacionado(int idInterfaz, int idBitacoraInterfaz)
        {
            var interfaz = GetOne(idInterfaz);
            if (interfaz.BitacoraInterfaz.Count > 0)
            {
                var comentario = interfaz.BitacoraInterfaz.Find(x => x.Id == idBitacoraInterfaz);
                if (comentario != null)
                {
                    UnitOfWork.Repository<BitacoraInterfaz>().Delete(idBitacoraInterfaz);

                    interfaz.FechaModificacion = DateTime.Now;
                    interfaz.ModificadoPor = Security.FullNameCurrentUser;
                    UpdateAndSave(interfaz);
                }
            }
        }
    }
}
