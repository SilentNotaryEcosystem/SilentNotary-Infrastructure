using System.Threading.Tasks;

namespace SilentNotary.Common
{
    public interface IIntegrationEventHandler<in TEvent>
        where TEvent: IIntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}