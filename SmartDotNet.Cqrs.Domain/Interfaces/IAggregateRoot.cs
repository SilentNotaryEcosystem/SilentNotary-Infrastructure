using System;
using System.Collections.Generic;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IAggregateRoot : IHasKey<Guid>
    {
        IEnumerable<IDomainEvent> DomainEvents { get; }
    }
}
