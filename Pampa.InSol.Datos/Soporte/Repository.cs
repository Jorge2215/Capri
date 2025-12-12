using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities;

namespace Pampa.InSol.Dal.Soporte
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDBContext context;

        private readonly DbSet<TEntity> dbentitySet;

        public Repository(AppDBContext context)
        {
            this.context = context;
            this.dbentitySet = context.Set<TEntity>();
        }

        public virtual TEntity Create(TEntity entity)
        {
            var newEntity = this.dbentitySet.Add(entity);

            return newEntity;
        }

        public virtual IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities)
        {
            var newEntities = new List<TEntity>();

            foreach (var entity in entities)
            {
                newEntities.Add(this.Create(entity));
            }

            return newEntities;
        }

        public virtual TEntity Delete(object id)
        {
            TEntity entityToDelete = this.dbentitySet.Find(id);

            if (entityToDelete == null)
            {
                var name = typeof(TEntity).Name;

                throw new ApplicationException(string.Format("La entidad '{0}' con ID {1} no existe.", name, id.ToString()));
            }

            return this.Delete(entityToDelete);
        }

        public virtual TEntity Delete(TEntity entityToDelete)
        {
            if (this.context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbentitySet.Attach(entityToDelete);
            }

            var deletedEntity = this.dbentitySet.Remove(entityToDelete);
            return deletedEntity;
        }

        public virtual TEntity GetById(object id)
        {
            return this.dbentitySet.Find(id);
        }

        public virtual TEntity Update(TEntity entityToUpdate)
        {
            var entityUpdated = this.dbentitySet.Attach(entityToUpdate);
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
            return entityUpdated;
        }

        public virtual IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            foreach (var entity in entitiesToUpdate)
            {
                this.Update(entity);
            }

            return entitiesToUpdate;
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return (IQueryable<TEntity>)this.dbentitySet;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.dbentitySet.AsEnumerable<TEntity>();
        }

        public TEntity GetBy(Func<TEntity, bool> predicate)
        {
            return this.dbentitySet.FirstOrDefault(predicate);
        }
    }
}
