using CSharpFunctionalExtensions;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IEventDispatcher
    {
        Result Dispatch<T>(T @event) where T : IDomainEvent;
    }
}
