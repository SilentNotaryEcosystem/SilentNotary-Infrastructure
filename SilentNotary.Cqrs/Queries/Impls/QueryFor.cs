using System.Threading.Tasks;
using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;

namespace SilentNotary.Cqrs.Queries.Impls
{
    /// <summary>
    ///     Стандартная реализация <see cref="SilentNotary.Cqrs.Queries.IQueryFor{T}" />
    /// </summary>
    /// <typeparam name="TResult">Результат возвращаемый запросом</typeparam>
    public class QueryFor<TResult> : IQueryFor<TResult>
    {
        private readonly IQueryFactory _factory;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="factory"></param>
        public QueryFor(IQueryFactory factory)
        {
            _factory = factory;
        }

        public TResult With<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion
        {
            return AsyncHelpers.RunSync(() => WithAsync(criterion));
        }

        public async Task<TResult> WithAsync<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion
        {
            return await _factory
                .Create<TCriterion, TResult>()
                .Ask(criterion);
        }
    }
}