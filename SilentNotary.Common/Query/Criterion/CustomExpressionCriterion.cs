using SilentNotary.Common.Query.Criterion.Abstract;
using System;
using System.Linq.Expressions;

namespace SilentNotary.Common.Query.Criterion
{
    public class CustomExpressionCriterion<T> : IExpressionCriterion<T>
    {
        private readonly Expression<Func<T, bool>> _expression;

        public CustomExpressionCriterion(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public Expression<Func<T, bool>> Get() => _expression;
        public T Value { get; set; }
    }
}
