using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SmartDotNet.Cqrs.Domain
{
    public abstract class AggregateRepository<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
    {
        public abstract Task<Result<TAggregate>> GetByIdAsync(TId id);

        public abstract void Add(TAggregate aggregate);

        public abstract void Update(TAggregate aggregate);
    }
}
