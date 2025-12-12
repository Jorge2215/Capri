using Pampa.InSol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pampa.InSol.Dal.Contratos.Soporte
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> Queryable();

        TEntity GetById(object id);

        TEntity GetBy(Func<TEntity, bool> predicate);

        TEntity Create(TEntity entity);

        IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        TEntity Delete(object id);

        TEntity Delete(TEntity entityToDelete);

        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
    }
}
