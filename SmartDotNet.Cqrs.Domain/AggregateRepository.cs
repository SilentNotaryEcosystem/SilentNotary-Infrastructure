using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using SmartDotNet.Specifications;

namespace SmartDotNet.Cqrs.Domain
{
    public abstract class AggregateRepository<TAggregate, TId>
        where TAggregate : Aggregate<TId>
    {
        public abstract Task<Result<TAggregate>> GetByIdAsync(TId id);

        public abstract void Add(TAggregate aggregate);

        public abstract void Update(TAggregate aggregate);

        public abstract Task<Result<TAggregate>> GetSingleAsync<TAggregateState>(Specification<TAggregateState> specification = null);

        public abstract Task<bool> AnyAsync<TAggregateState>(Specification<TAggregateState> specification = null)
            where TAggregateState : DomainEntityBase<TId>;

        public abstract Task<IEnumerable<TAggregate>> GetAllAsync<TAggregateState>(Specification<TAggregateState> specification = null)
            where TAggregateState: DomainEntityBase<TId>;

        protected static Expression<Func<TDest, bool>> ConvertSpecification<TSrc, TDest>(Specification<TSrc> specification)
        {
            if (specification == null) return null;
            Expression<Func<TSrc, bool>> expr = specification;
            return Expression.Lambda<Func<TDest, bool>>(expr.Body, expr.Parameters);
        }
    }
}
