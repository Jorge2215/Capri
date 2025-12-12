using Pampa.InSol.Biz.Contratos.Servicios;
using Pampa.InSol.Dal.Contratos.Soporte;
using Pampa.InSol.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Pampa.InSol.Biz.Servicios
{
    public abstract class AbstractServicio<T> : BaseServicio, IServicio<T> where T : BaseEntity
    {
        public AbstractServicio(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public virtual bool DeleteAndSave(int id)
        {
            this.UnitOfWork.Repository<T>().Delete(id);
            this.UnitOfWork.SaveChanges();
            return true;
        }

        public virtual IQueryable<T> GetAll(Func<T, bool> func, params string[] includes)
        {
            var query = this.UnitOfWork.Repository<T>().GetAll().AsQueryable();
            if (includes != null)
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            return query.Where(func).AsQueryable();
        }

        public virtual IEnumerable<T> GetAllNoTracking(Func<T, bool> func, params string[] includes)
        {
            var query = this.UnitOfWork.Repository<T>().GetAll().AsQueryable();
            if (includes != null)
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            return query.AsNoTracking().Where(func);
        }

        public virtual IQueryable<T> GetAll(List<Expression<Func<T, object>>> includes, Func<T, bool> func)
        {
            var query = this.UnitOfWork.Repository<T>().GetAll().AsQueryable();
            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include).AsNoTracking();
                }
            return query.Where(func).AsQueryable();
        }

        public virtual IQueryable<T> GetAll(Func<T, bool> func, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = this.UnitOfWork.Repository<T>().GetAll().Where(func).AsQueryable();

            if (orderBy != null)
                return orderBy(query);

            return query;
        }

        public virtual IQueryable<T> GetAll(params string[] includes)
        {
            var query = this.UnitOfWork.Repository<T>().GetAll().AsQueryable();
            if (includes != null)
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            return query;
        }

        public IQueryable<T> GetAll()
        {
            var query = this.UnitOfWork.Repository<T>().GetAll().AsQueryable();
            return query;
        }

        public virtual T GetOne(int id, params string[] includes)
        {
            var query = this.UnitOfWork.Repository<T>().GetAll().AsQueryable();
            if (includes != null)
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            return query.FirstOrDefault(x => x.Id == id);
        }

        public virtual T GetOne(int id)
        {
            return this.UnitOfWork.Repository<T>().GetById(id);
        }

        public virtual T GetOne(Func<T, bool> predicate)
        {
            return this.UnitOfWork.Repository<T>().GetBy(predicate);
        }

        public virtual T Insert(T objeto)
        {
            return this.UnitOfWork.Repository<T>().Create(objeto);
        }

        public virtual T InsertAndSave(T objeto)
        {
            var entity = this.UnitOfWork.Repository<T>().Create(objeto);
            UnitOfWork.SaveChanges();
            return entity;
        }

        public virtual T Update(T objeto)
        {
            return this.UnitOfWork.Repository<T>().Update(objeto);

        }

        public virtual T UpdateAndSave(T objeto)
        {
            var answ = this.UnitOfWork.Repository<T>().Update(objeto);
            this.UnitOfWork.SaveChanges();
            return answ;
        }
    }
}
