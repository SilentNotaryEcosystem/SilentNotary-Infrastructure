using System.Collections.Generic;
using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;

namespace SilentNotary.Cqrs.Nats.Abstract
{
    public interface INatsQueueFactory
    {
        KeyValuePair<string, string> GetCommandQueue(IMessage message);
        KeyValuePair<string, string> GetCommandQueue();
        string GetQueryQueue(ICriterion message, object result);
        string GetQueryQueue();
    }
}