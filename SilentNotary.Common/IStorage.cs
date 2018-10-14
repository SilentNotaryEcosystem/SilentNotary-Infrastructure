using SilentNotary.Common.Query.Criterion.Abstract;
using System.Linq;
using System.Threading.Tasks;
using SilentNotary.Cqrs.Domain.Interfaces;

namespace SilentNotary.Common
{
    public interface IStorage<TEntity> where TEntity : class, IHasKey
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(IExpressionCriterion<TEntity> condition);
        void Add(TEntity data);
        void Remove(TEntity data);
        Task Save(params TEntity[] messages);
    }
}
