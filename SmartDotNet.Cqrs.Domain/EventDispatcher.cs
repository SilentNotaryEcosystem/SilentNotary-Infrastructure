using System.Linq;
using System.Reflection;
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

        public Task<Result> Dispatch(IDomainEvent @event)
        {
            return
                ParametersValidation.NotNull(@event, nameof(@event))
                    .OnSuccess(() =>
                    {
                        var eventType = @event.GetType();
                        var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        var handler = _context.ResolveOptional(handlerType);

                        if (handler == null) return Task.FromResult(Result.Ok());

                        var method = handler.GetType()
                            .GetRuntimeMethods()
                            .First(x => x.Name.Equals("HandleAsync")
                                        && x.GetParameters().First().ParameterType == eventType);

                        return (Task<Result>) method.Invoke(handler, new object[] { @event });
                    })
                ;
        }
    }
}