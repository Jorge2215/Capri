using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Biz.Seguridad;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Pampa.InSol.Biz.Servicios
{
    public class RolServicio : AbstractServicio<Rol>, IRolServicio
    {
        public RolServicio(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IDictionary<string, Rol> GetRolesActivos()
        {
            //// Armo un diccionario con todos los roles activos desde la base de datos.
            return this.UnitOfWork.Repository<Rol>().Queryable().Where(r => r.Activo).ToDictionary(x => x.Descripcion);
        }

        public IList<string> GetDescripciones()
        {
            var descripciones = this.UnitOfWork.Repository<Rol>().Queryable().Where(r => r.Activo).Select(r => r.Descripcion);
            return descripciones.ToList();
        }

        public void Save(RolViewModel rolModel, List<int> funciones)
        {
            Rol rolSave = new Rol();

            if (!ValidateDuplicated(rolModel.Id, rolModel.Descripcion))
            {
                throw new Exception("Ya existe el Rol con la descripcion seleccionada");
            }

            if (rolModel.Id != 0)
            {
                rolSave = GetOne(rolModel.Id);
            }

            rolSave.Descripcion = rolModel.Descripcion;
            rolSave.Activo = rolModel.Activo;
            rolSave.CreadoPor = Security.UserNT;
            rolSave.ModificadoPor = Security.UserNT;
            rolSave.FechaCreacion = DateTime.Now;
            rolSave.FechaModificacion = DateTime.Now;

            List<Funcion> funcionesEliminar = rolSave.Funciones.Where(f => (funciones == null || !funciones.Contains(f.Id))).ToList();

            foreach (Funcion funcion in funcionesEliminar)
            {
                rolSave.Funciones.Remove(funcion);
            }

            if (funciones != null)
            {
                foreach (int idFuncion in funciones)
                {
                    if (!rolSave.Funciones.Any(f => f.Id == idFuncion))
                    {
                        rolSave.Funciones.Add(this.UnitOfWork.Repository<Funcion>().GetById(idFuncion));
                    }
                }
            }
            if (rolModel.Id == 0)
            {
                InsertAndSave(rolSave);
            }
            else
            {
                UpdateAndSave(rolSave);
            }
        }

        public bool ValidateDuplicated(int? id, string desc)
        {
            return !GetAll().Any(r => r.Descripcion.Equals(desc, StringComparison.InvariantCultureIgnoreCase) && (!id.HasValue || id.Value != r.Id));
        }

        public List<FuncionViewModel> GetFunciones(int? idRol)
        {
            return this.UnitOfWork.Repository<Funcion>().GetAll().OrderBy(x => x.Id).Select(f => new FuncionViewModel
            {
                Id = f.Id,
                Descipcion = f.Descripcion,
                IdPadre = f.IdPadre,
                Asignado = (!idRol.HasValue ? false : f.Roles.Any(r => (r.Id == idRol))),
            }).ToList();
        }

        public List<SelectListItem> GetRolesAsignadosByUsuario(int idUsuario)
        {
            return GetAll(r => r.Usuarios.Any(u => u.Id == idUsuario)).Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Descripcion
            }).ToList();
        }
        public List<SelectListItem> GetRolesDisponiblesByUsuario(int? idUsuario = null)
        {
            return GetAll(r => (!idUsuario.HasValue || !r.Usuarios.Any(u => u.Id == idUsuario))).Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Descripcion
            }).ToList();
        }
    }
}
