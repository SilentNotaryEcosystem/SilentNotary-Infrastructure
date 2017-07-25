using System.Collections.Generic;
using System.Linq;
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

        public Result Dispatch<T>(T @event) where T : IDomainEvent
        {
            return             
                ParametersValidation.NotNull(@event, nameof(@event))            
            .OnSuccess(() =>
                {                    
                    var handler =_context.ResolveOptional<IEventHandler<T>>();
                    return handler?.Handle(@event) ?? Result.Ok();
                })
            ;            
        }
    }
}
