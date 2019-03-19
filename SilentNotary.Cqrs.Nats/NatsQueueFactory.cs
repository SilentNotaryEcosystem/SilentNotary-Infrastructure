using System.Collections.Generic;
using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;
using SilentNotary.Cqrs.Nats.Abstract;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsQueueFactory : INatsQueueFactory
    {
        public KeyValuePair<string, string> GetCommandQueue(IMessage message)
        {
            return GetCommandQueue();
        }

        public KeyValuePair<string, string> GetCommandQueue()
        {
            return new KeyValuePair<string, string>("ComandsSubject", "ComandsQueue");
        }

        public string GetQueryQueue(ICriterion message, object result)
        {
            return GetQueryQueue();
        }

        public string GetQueryQueue()
        {
            return "QuerySubject";
        }
    }
}