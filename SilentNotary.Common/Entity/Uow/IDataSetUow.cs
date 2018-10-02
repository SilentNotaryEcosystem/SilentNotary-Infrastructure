using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SilentNotary.Cqrs.Domain.Interfaces;

namespace SilentNotary.Common.Entity.Uow
{
    public interface IDataSetUow
    {
        IQueryable<T> GetQuery<T>() where T : class, IHasKey;
        IQueryable Query(Type type);
        IQueryable<T> Include<T, TProp>(IQueryable<T> queryable, params Expression<Func<T, TProp>>[] paths) where T : class;
        object GetContext();
        TEntity Find<TEntity>(object id) where TEntity : class;
        Task<TEntity> FindAsync<TEntity>(object id) where TEntity : class;


        /* CRUD */
        void AddEntity<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entity) where T : class;
        void RemoveEntity<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entity) where T : class;
        int Commit();
        Task<int> CommitAsync();
        void FixupState<T>(T entity) where T : class;
    }
}
