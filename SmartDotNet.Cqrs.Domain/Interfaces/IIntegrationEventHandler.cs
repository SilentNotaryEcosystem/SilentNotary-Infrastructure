using System.Threading.Tasks;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IIntegrationEventHandler<in TEvent>
        where TEvent: IIntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}