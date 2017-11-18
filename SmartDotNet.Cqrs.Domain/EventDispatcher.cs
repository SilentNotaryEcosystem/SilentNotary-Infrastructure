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

        public async Task<Result> Dispatch(IDomainEvent @event)
        {
            var validationResult =
                ParametersValidation.NotNull(@event, nameof(@event));
            if (validationResult.IsFailure)
                return validationResult;

            var eventType = @event.GetType();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handler = _context.ResolveOptional(handlerType);

            if (handler == null) return Result.Ok();

            var method = handler.GetType()
                .GetRuntimeMethods()
                .First(x => x.Name.Equals("HandleAsync")
                            && x.GetParameters().First().ParameterType == eventType);

            return await (Task<Result>) method.Invoke(handler, new object[] { @event });
    }
}

}