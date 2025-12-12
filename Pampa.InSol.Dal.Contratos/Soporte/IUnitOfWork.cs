using System;
using System.Data.Entity;
using Pampa.InSol.Entities;

namespace Pampa.InSol.Dal.Contratos.Soporte
{
    public interface IUnitOfWork : IStoredProcedureInvoker, IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        void SaveChanges();

        DbContextTransaction IniciarTransacion();

        DbContextTransaction ObtenerTransaccionActiva();
    }
}
