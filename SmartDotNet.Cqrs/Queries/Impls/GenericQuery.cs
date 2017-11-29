using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SmartDotNet.Cqrs.Queries.Impls
{
    public class GenericQuery<TSource> : IGenericQuery<TSource>
        where TSource : class
    {
        protected IQueryable<TSource> Queryable;

        internal GenericQuery(IQueryable<TSource> queryable)
        {
            Queryable = queryable;
        }

        public Task<TSource> FirstOrDefaultAsync()
        {
            return Queryable.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TSource>> AllAsync()
        {
            return await Queryable.ToArrayAsync();
        }

        public async Task<IPagedList<TSource>> PagedAsync(int pageNumber, int pageSize)
        {
            return await Queryable.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
