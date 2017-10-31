using System;
using System.Linq.Expressions;

namespace SmartDotNet.Cqrs.Specifications.Helpers
{
    public class AdHocSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _predicate;

        public AdHocSpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentException(nameof(predicate));
        }

        internal override Expression<Func<T, bool>> ToExpression()
        {
            return _predicate;
        }
    }
}
