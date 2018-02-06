using System;
using System.Linq.Expressions;
using In.Legacy.Query.Criterion.Abstract;

namespace In.Legacy.Query.Criterion
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
