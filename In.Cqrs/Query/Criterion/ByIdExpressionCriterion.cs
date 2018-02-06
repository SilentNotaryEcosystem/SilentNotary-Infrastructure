using System;
using System.Linq.Expressions;
using In.Legacy.Query.Criterion.Abstract;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace In.Legacy.Query.Criterion
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