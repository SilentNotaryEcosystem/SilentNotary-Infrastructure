using System;
using System.Linq;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace SmartDotNet.Cqrs.Queries
{
    /// <summary>
    /// </summary>
    public interface ILinqProvider
    {
        /// <summary>
        ///     Query object for concrete <see cref="IHasKey" />
        /// </summary>
        /// <typeparam name="TEntity">
        ///     <see cref="IHasKey" />
        /// </typeparam>
        /// <returns>
        ///     <see cref="IQueryable{TEntity}" /> object for type of TEntity
        /// </returns>
        IQueryable<TEntity> Query<TEntity>()
            where TEntity : class, IHasKey;

        IQueryable Query(Type type);
    }
}