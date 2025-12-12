using NLog;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Pampa.InSol.Dal.Soporte
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext context;
        private bool disposed = false;
        private Hashtable repositories;

        public UnitOfWork(AppDBContext context)
        {
            this.context = context;
        }

        protected Logger Logger
        {
            get
            {
                return LogManager.GetCurrentClassLogger();
            }
        }

        public virtual IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (this.repositories == null)
            {
                this.repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;
                        if (this.repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)this.repositories[type];
            }

            Type repositoryType = typeof(Repository<>);
                        this.repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), this.context));
                        return (IRepository<TEntity>)this.repositories[type];
        }

        public virtual void SaveChanges()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                this.Logger.Warn("Error de validación en EF al querer salvar una transacción", ex);
                throw;
            }
        }

        public virtual DbContextTransaction IniciarTransacion()
        {
            return this.context.Database.BeginTransaction();
        }

        public virtual DbContextTransaction ObtenerTransaccionActiva()
        {
            return this.context.Database.CurrentTransaction;
        }

        public virtual List<T> InvocarStoreProcedureDinamico<T>(string nombre, Dictionary<string, object> parametros)
        {
            throw new NotImplementedException();
        }

        public virtual T InvocarFuncion<T>(string nombre, Dictionary<string, object> parametros)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
