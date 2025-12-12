using NLog;
using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities.Entities;
using Pampa.InSol.Entities.Models;
using System;
using System.Linq;
using System.Data.Entity;

namespace Pampa.InSol.Biz.Servicios
{
    public class ProductoAmbienteServicio : AbstractServicio<ProductoAmbiente>, IProductoAmbienteServicio
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        public ProductoAmbienteServicio(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public void Delete(ProductoAmbiente obj)
        {
            var a = this.UnitOfWork.Repository<ProductoAmbiente>().Delete(obj);
            UnitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProductoAmbiente GetProductoAmbienteByIdProducto(int id)
        {
            var prodAmbiente = this.UnitOfWork.Repository<ProductoAmbiente>().Queryable().Where(u => u.IdProducto == id).AsNoTracking().FirstOrDefault();
            return prodAmbiente;
        }

        public void Save(ProductoAmbienteModel entity)
        {
            var entityDB = GetOne(x => x.Id == entity.Id);

            if (entity.Id == 0)
            {
                ProductoAmbiente productoAmbiente = new ProductoAmbiente()
                {
                    IdAmbiente = entity.IdAmbiente,
                    IdRolAmbiente = entity.IdRolAmbiente,
                    IdProducto = entity.IdProducto,
                    Server = entity.Server,
                    Nombre = entity.Nombre,
                    Usuario = entity.Usuario,
                    UsuarioEmer = entity.UsuarioEmer,
                    Comentario = entity.Comentario
                };

                UnitOfWork.Repository<ProductoAmbiente>().Create(productoAmbiente);
                UnitOfWork.SaveChanges();

            }
            else
            {
                entityDB.IdAmbiente = entity.IdAmbiente;
                entityDB.IdRolAmbiente = entity.IdRolAmbiente;
                entityDB.IdProducto = entity.IdProducto;
                entityDB.Server = entity.Server;
                entityDB.Nombre = entity.Nombre;
                entityDB.Usuario = entity.Usuario;
                entityDB.UsuarioEmer = entity.UsuarioEmer;
                entityDB.Comentario = entity.Comentario;

                UnitOfWork.Repository<ProductoAmbiente>().Update(entityDB);
                UnitOfWork.SaveChanges();
        }

    }
}
}
