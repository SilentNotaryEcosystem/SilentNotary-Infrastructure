using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace SmartDotNet.Cqrs.Queries
{
    public interface IGenericQuery<T>
    {
        Task<T> FirstOrDefaultAsync();

        Task<IEnumerable<T>> AllAsync();

        Task<IPagedList<T>> PagedAsync(int pageNumber, int pageSize);
    }
}
