using Pampa.InSol.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Biz.Contratos.Servicios
{
    public interface IServicio<T> where T : BaseEntity
    {
        /// <summary>
        /// Obtiene todos los elementos junto con sus include
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(params string[] includes);
        /// <summary>
        /// Obtiene todos los elementos filtrados, junto con sus include
        /// </summary>
        /// <param name="includes"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Func<T, bool> func, params string[] includes);
        IEnumerable<T> GetAllNoTracking(Func<T, bool> func, params string[] includes);
        IQueryable<T> GetAll(List<Expression<Func<T, object>>> includes, Func<T, bool> func);
        /// <summary>
        /// Obtiene todos los elementos filtrados
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Func<T, bool> func, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        /// <summary>
        /// Obtiene todos los elementos
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        T GetOne(int id, params string[] includes);
        T GetOne(int id);
        T GetOne(Func<T, bool> predicate);
        T Insert(T objeto);
        T InsertAndSave(T objeto);
        T Update(T objeto);
        T UpdateAndSave(T objeto);
        bool DeleteAndSave(int id);
    }
}
