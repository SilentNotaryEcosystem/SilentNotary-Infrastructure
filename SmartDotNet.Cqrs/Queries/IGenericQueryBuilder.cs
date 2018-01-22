using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartDotNet.Cqrs.Domain.Interfaces;
using SmartDotNet.Specifications;
using X.PagedList;

namespace SmartDotNet.Cqrs.Queries
{
    public interface IGenericQueryBuilder<TSource> where TSource : class, IHasKey
    {
        IGenericQuery<TDest> ProjectTo<TDest>()
            where TDest : class;

        IGenericQuery<TDest> Select<TDest>(Expression<Func<TSource, TDest>> selector)
            where TDest : class;

        IGenericQueryBuilder<TSource> Where(Specification<TSource> specification);

        IGenericQueryBuilder<TSource> OrderBy<TKey>(Expression<Func<TSource, TKey>> keySelector,
            bool descending = false);

        Task<int> CountAsync();
        Task<TSource> MinAsync();
        Task<TSource> MaxAsync();
        Task<TSource> FirstOrDefaultAsync();
        Task<IEnumerable<TSource>> AllAsync();
        Task<IPagedList<TSource>> PagedAsync(int pageNumber, int pageSize);
    }
}