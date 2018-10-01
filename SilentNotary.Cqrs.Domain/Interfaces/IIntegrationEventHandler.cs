using System.Threading.Tasks;

namespace SilentNotary.Cqrs.Domain.Interfaces
{
    public interface IIntegrationEventHandler<in TEvent>
        where TEvent: IIntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}