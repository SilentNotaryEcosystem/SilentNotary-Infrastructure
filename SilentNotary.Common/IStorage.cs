using SilentNotary.Common.Query.Criterion.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace SilentNotary.Common
{
    public interface IStorage<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(IExpressionCriterion<T> condition);
        void Add(T data);
        void Remove(T data);
        Task Save(params T[] messages);
    }
}
