using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace SmartDotNet.Cqrs.Domain
{
    public abstract class AggregateRoot<TId> : DomainEntityBase<TId>, IAggregateRoot
    {
        private readonly IDictionary<Type, IDomainEvent> _events = new ConcurrentDictionary<Type, IDomainEvent>();

        public IEnumerable<IDomainEvent> DomainEvents => _events.Values;

        protected void AddEvent(IDomainEvent @event)
        {
            _events[@event.GetType()] = @event;
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
