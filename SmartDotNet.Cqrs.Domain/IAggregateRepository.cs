using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SmartDotNet.Cqrs.Domain
{
    public interface IAggregateRepository<TAggregate>
        where TAggregate : Aggregate
    {
        Task<Result<TAggregate>> GetByIdAsync(Guid id);
    }
}
