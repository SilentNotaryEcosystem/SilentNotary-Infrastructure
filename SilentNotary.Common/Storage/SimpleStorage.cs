using System.Linq;
using System.Threading.Tasks;
using SilentNotary.Common.Command;
using SilentNotary.Common.Entity.Uow;
using SilentNotary.Common.Query;
using SilentNotary.Common.Query.Criterion.Abstract;

namespace SilentNotary.Common.Storage
{
    public class SimpleStorage<TEntity> : IStorage<TEntity> where TEntity : class, IHasKey
    {
        private readonly IDiScope _diScope;

        public SimpleStorage(IDiScope diScope/*<ExpressionQuery> simpleQuery, SaveCommandHandler<TEntity, TKey> saveCommand, DeleteCommandHandler<TEntity, TKey> deleteCommand, IDataSetUow dataSetUow*/)
        {
            _diScope = diScope;
        }

        public IQueryable<TEntity> Get(IExpressionCriterion<TEntity> condition)
        {
            var simpleQuery = _diScope.Resolve<ExpressionQuery>();
            return simpleQuery
                .Ask(condition);
        }
        public IQueryable<TEntity> GetAll()
        {
            return _diScope
                .Resolve<IDataSetUow>()
                .GetQuery<TEntity>();
        }

        public void Add(TEntity data)
        {
            var dataSetUow = _diScope.Resolve<IDataSetUow>();
            dataSetUow.AddEntity(data);
        }

        public async Task Save(params TEntity[] messages)
        {
            var saveCommand = _diScope.Resolve<SaveCommandHandler<TEntity>>();
            await saveCommand.Handle(messages);
        }

        public void Remove(TEntity data)
        {
            var deleteCommand = _diScope.Resolve<DeleteCommandHandler<TEntity>>();
            deleteCommand.Handle(data);
        }

    }
}