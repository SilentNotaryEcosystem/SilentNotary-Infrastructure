using System;
using System.Linq.Expressions;

namespace SmartDotNet.Cqrs.Specifications.Helpers.BooleanOperators
{
    internal class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec;

        internal NotSpecification(Specification<T> spec)
        {
            _spec = spec ?? throw new ArgumentException(nameof(spec)); 
        }

        internal override Expression<Func<T, bool>> ToExpression()
        {
            var expr = _spec.ToExpression();
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);
        }
    }
}
