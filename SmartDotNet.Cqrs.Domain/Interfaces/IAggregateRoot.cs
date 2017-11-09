using System.Collections.Generic;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IAggregateRoot 
    {
        IEnumerable<IDomainEvent> DomainEvents { get; }
        void ClearEvents();
    }
}
