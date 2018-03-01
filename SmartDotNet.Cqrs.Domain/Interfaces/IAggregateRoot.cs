using System.Collections.Generic;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IAggregateRoot 
    {
        IEnumerable<IDomainEvent> DomainEvents { get; }
        IEnumerable<IDomainEvent> CommitedDomainEvents { get; }
        void OnEventsCommited();
        void ClearCommitedEvents();
    }
}
