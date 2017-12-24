using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IDddUnitOfWork
    {
        AggregateRepository<TAggregate, TId> Repository<TAggregate, TId>()
            where TAggregate : AggregateRoot<TId>;

        IValueObjectProvider ValueObjectProvider { get; }

        Task<Result> CommitAsync();
    }
}
