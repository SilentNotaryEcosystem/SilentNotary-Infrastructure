using System.Collections.Generic;
using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;

namespace SilentNotary.Cqrs.Nats.Abstract
{
    public interface INatsSenderQueueFactory
    {
        KeyValuePair<string, string> GetCommandQueue(IMessage message);
        string GetQueryQueue(ICriterion message, object result);
    }

    public interface INatsReceiverQueryQueueFactory
    {
        string Get();
    }
    
    public interface INatsReceiverCommandQueueFactory
    {
        KeyValuePair<string, string> Get();
    }
}