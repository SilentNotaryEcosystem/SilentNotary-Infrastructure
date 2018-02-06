using System;
using System.Collections.Generic;

namespace In.Legacy
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion);
        List<IEvent> GetEventsForAggregate(Guid aggregateId);
    }
}
