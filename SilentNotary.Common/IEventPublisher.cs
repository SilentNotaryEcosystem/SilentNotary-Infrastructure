
namespace SilentNotary.Common
{
    public interface IEventPublisher
    {
        void Publish<T>(T ev) where T : IEvent;
    }
}