using System;
using System.Linq;
using In.Legacy.Entity.Uow;
using In.Legacy.Query.Criterion.Abstract;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace In.Legacy.Query
{
    [Obsolete("use specifications way")]
    public class ExpressionQuery
    {
        private readonly IDataSetUow _dataSetUow;

        public ExpressionQuery(IDataSetUow dataSetUow)
        {
            _dataSetUow = dataSetUow;
        }

        public IQueryable<T> Ask<T>(IExpressionCriterion<T> criterion) where T : class, IHasKey
        {
            return _dataSetUow
                .Query<T>()
                .Where(criterion.Get());
        }
    }
}