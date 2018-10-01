using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using SilentNotary.Cqrs.Domain.Interfaces;

namespace SilentNotary.Cqrs.Domain.Interfaces
{
    public interface IEventDispatcher
    {
        Task<Result> Dispatch(IDomainEvent @event);
    }
}
