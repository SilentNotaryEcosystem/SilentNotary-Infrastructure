using SilentNotary.Common.Query.Criterion.Abstract;
using SilentNotary.Cqrs.Domain.Interfaces;
using System;
using System.Linq.Expressions;

namespace SilentNotary.Common.Query.Criterion
{
    public class EmptyExpressionCriterion<T> : IExpressionCriterion<T> where T : class, IHasKey<int>
    {
        public Expression<Func<T, bool>> Get()
        {
            return entity => true;
        }

        public T Value { get; set; }
    }
}
