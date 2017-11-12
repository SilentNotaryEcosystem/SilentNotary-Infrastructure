using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IEventDispatcher
    {
        Task<Result> Dispatch<T>(T @event) where T : IDomainEvent;
    }
}
