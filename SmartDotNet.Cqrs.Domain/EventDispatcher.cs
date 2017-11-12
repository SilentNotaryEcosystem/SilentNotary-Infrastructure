using System.Threading.Tasks;
using Autofac;
using CSharpFunctionalExtensions;
using SmartDotNet.Cqrs.Domain.Interfaces;
using SmartDotNet.FunctionalCSharp;

namespace SmartDotNet.Cqrs.Domain
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;

        public EventDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public Task<Result> Dispatch<T>(T @event) where T : IDomainEvent
        {
            return
                ParametersValidation.NotNull(@event, nameof(@event))
                    .OnSuccess(() =>
                    {
                        var handler = _context.ResolveOptional<IEventHandler<T>>();
                        if (handler != null)
                            return handler.Handle(@event);

                        return Task.FromResult(Result.Ok());
                    })
                ;
        }
    }
}