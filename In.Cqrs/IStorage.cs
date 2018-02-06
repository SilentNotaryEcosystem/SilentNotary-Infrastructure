using System.Linq;
using System.Threading.Tasks;
using In.Legacy.Query.Criterion.Abstract;

namespace In.Legacy
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
