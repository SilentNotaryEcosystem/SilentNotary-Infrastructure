using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SmartDotNet.Cqrs.Domain
{
    public interface IAggregateRepository<TRoot>
        where TRoot : IAggregateRoot
    {
        Task<Result<TRoot>> GetByIdAsync(Guid id);
    }
}
