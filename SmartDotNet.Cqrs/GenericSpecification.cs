using System;
using System.Linq.Expressions;
using SmartDotNet.Specifications;

namespace SmartDotNet.Cqrs
{
    internal class GenericSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _predicate;

        internal GenericSpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentException(nameof(predicate));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _predicate;
        }
    }
}
