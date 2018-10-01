using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SilentNotary.Cqrs.Domain.Interfaces
{
    public interface IEventHandler<in T> where T : IDomainEvent
    {
        Task<Result> HandleAsync(T @event);
    }
}
