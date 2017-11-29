using In.Di;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace SmartDotNet.Cqrs.Queries.Impls
{
    public class QueryBuilder : IQueryBuilder
    {
        private readonly IDiScope _diScope;

        public QueryBuilder(IDiScope diScope)
        {
            _diScope = diScope;
        }

        public IQueryFor<TResult> For<TResult>()
        {
            return _diScope.Resolve<IQueryFor<TResult>>();
        }

        public IGenericQueryBuilder<TSource> Generic<TSource>() where TSource : class, IHasKey
        {
            return _diScope.Resolve<IGenericQueryBuilder<TSource>>();
        }
    }
}