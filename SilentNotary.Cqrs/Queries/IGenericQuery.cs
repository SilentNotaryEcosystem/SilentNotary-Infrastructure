using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace SilentNotary.Cqrs.Queries
{
    public interface IGenericQuery<T>
    {
        Task<T> FirstOrDefaultAsync();

        Task<IEnumerable<T>> AllAsync();

        Task<IPagedList<T>> PagedAsync(int pageNumber, int pageSize);

        Task<decimal> SumAsync(Expression<Func<T, decimal>> expression);
    }
}
