using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using In.Legacy;
using SmartDotNet.Cqrs.Domain;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace SmartDotNet.Cqrs.Commands
{
    public abstract class CommandHandlerBase<TAggregate, TKey>
        where TAggregate : Aggregate<TKey>
    {
        protected readonly IDddUnitOfWork UnitOfWork;
        protected readonly AggregateRepository<TAggregate, TKey> Repository;
        protected readonly IMessageSender _messageSender;

        public CommandHandlerBase(IDddUnitOfWork unitOfWork, IMessageSender messageSender)
        {
            UnitOfWork = unitOfWork;
            Repository = UnitOfWork.Repository<TAggregate, TKey>();
            _messageSender = messageSender;
        }

        /// <summary>
        /// Updates aggregate, commits unit of work and publishes integration events from aggregate's
        /// <seealso cref="Aggregate{TId}.DomainEvents"/> collection
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        protected Task<Result> SaveChangesAndPublishEvents(TAggregate aggregate)
        {
            Repository.Update(aggregate);
            return UnitOfWork.CommitAsync()
                .OnSuccess(async () => await PublishIntegrationEvents(aggregate.DomainEvents));
        }

        /// <summary>
        /// Updates aggregates, commits unit of work and publishes integration events from all aggregate's
        /// <seealso cref="Aggregate{TId}.DomainEvents"/> collections
        /// </summary>
        /// <param name="aggregates"></param>
        /// <returns></returns>
        protected Task<Result> SaveChangesAndPublishEvents(IEnumerable<TAggregate> aggregates)
        {
            var aggregateArray = aggregates.ToArray();
            foreach (var aggregate in aggregateArray)
                Repository.Update(aggregate);
            return UnitOfWork.CommitAsync()
                .OnSuccess(async () => await PublishIntegrationEvents(
                    aggregateArray.SelectMany(aggregate => aggregate.DomainEvents)));
        }

        /// <summary>
        /// Helper method which scans collection of domain events and publishes events 
        /// that are integration events to event bus
        /// </summary>
        /// <param name="domainEvents">collection of domain events</param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        private async Task PublishIntegrationEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var @event in domainEvents)
            {
                if (!(@event is IIntegrationEvent)) continue;

                var publishMethod = _messageSender
                    .GetType()
                    .GetRuntimeMethods()
                    .First(x => x.Name == nameof(IMessageSender.PublishAsync));
                await (Task)publishMethod
                    .MakeGenericMethod(@event.GetType())
                    .Invoke(_messageSender, new object[] { @event });
            }
        }
    }
}