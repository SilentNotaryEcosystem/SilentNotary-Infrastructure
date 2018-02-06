using System;
using System.Linq.Expressions;
using In.Legacy.Query.Criterion.Abstract;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace In.Legacy.Query.Criterion
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
