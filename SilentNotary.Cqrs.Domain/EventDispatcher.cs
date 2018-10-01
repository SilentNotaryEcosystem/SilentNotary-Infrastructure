using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using SilentNotary.Cqrs.Domain.Interfaces;
using SilentNotary.FunctionalCSharp;

namespace SilentNotary.Cqrs.Domain
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Result> Dispatch(IDomainEvent @event)
        {
            var validationResult =
                ParametersValidation.NotNull(@event, nameof(@event));
            if (validationResult.IsFailure)
                return validationResult;

            var eventType = @event.GetType();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null) return Result.Ok();

            var method = handler.GetType()
                .GetRuntimeMethods()
                .First(x => x.Name.Equals("HandleAsync")
                            && x.GetParameters().First().ParameterType == eventType);

            return await (Task<Result>) method.Invoke(handler, new object[] { @event });
    }
}

}