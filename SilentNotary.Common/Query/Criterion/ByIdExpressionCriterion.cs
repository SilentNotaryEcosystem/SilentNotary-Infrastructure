using SilentNotary.Common.Query.Criterion.Abstract;
using System;
using System.Linq.Expressions;

namespace SilentNotary.Common.Query.Criterion
{
    public class ByIdExpressionCriterion<T, TId> : IExpressionCriterion<T> where T : class, IHasKey<TId>
    {
        private readonly TId _id;

        public ByIdExpressionCriterion(TId id)
        {
            _id = id;
        }

        public Expression<Func<T, bool>> Get()
        {
            return entity => (object) entity.Id == (object) _id;
        }

        public T Value { get; set; }
    }
}