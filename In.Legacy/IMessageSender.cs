using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace In.Legacy
{
    public interface IMessageSender
    {
        Result Send<T>(T command) where T : IMessage;
        Task<Result> SendAsync<TInput>(TInput command) where TInput : IMessage;
        Task<Result<TOutput>> SendAsync<TInput, TOutput>(TInput command) where TInput : IMessage;
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IIntegrationEvent;
    }
}
