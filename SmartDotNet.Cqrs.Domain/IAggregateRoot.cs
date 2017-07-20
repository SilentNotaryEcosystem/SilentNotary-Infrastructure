using System;

namespace SmartDotNet.Cqrs.Domain
{
    public interface IAggregateRoot : IHasKey<Guid>
    {
    }
}
