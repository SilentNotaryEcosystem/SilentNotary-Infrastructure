using CSharpFunctionalExtensions;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IEventHandler<in T> where T : IDomainEvent
    {
        Result Handle(T @event);
    }
}
