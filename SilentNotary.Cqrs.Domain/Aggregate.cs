using SilentNotary.Cqrs.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SilentNotary.Cqrs.Domain
{
    public abstract class Aggregate<TId> : DomainEntityBase<TId>, IAggregateRoot
    {
        private readonly IDictionary<Type, IDomainEvent> _events = new ConcurrentDictionary<Type, IDomainEvent>();
        private readonly IDictionary<Type, IDomainEvent> _commitedEvents = new ConcurrentDictionary<Type, IDomainEvent>();

        public IEnumerable<IDomainEvent> DomainEvents => _events.Values;
        public IEnumerable<IDomainEvent> CommitedDomainEvents => _commitedEvents.Values;

        protected void AddEvent(IDomainEvent @event)
        {
            _events[@event.GetType()] = @event;
        }
        
        public void OnEventsCommited()
        {
            var events = _events.Values.ToArray();
            _events.Clear();
            foreach (var @event in events)
            {
                _commitedEvents[@event.GetType()] = @event;
            }
        }

        public void ClearCommitedEvents()
        {
            _commitedEvents.Clear();
        }
    }
}
