using System.Collections.Generic;
using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;
using SilentNotary.Cqrs.Nats.Abstract;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsQueueFactory : INatsSenderQueueFactory
    {
        private INatsReceiverQueryQueueFactory _natsReceiverQueryQueueFactoryImplementation;

        public KeyValuePair<string, string> GetCommandQueue(IMessage message) =>
            new KeyValuePair<string, string>("ComandsSubject", "ComandsQueue");

        public string GetQueryQueue(ICriterion message, object result) => "QuerySubject";
    }

    public class NatsReceiverQueryQueueFactory : INatsReceiverQueryQueueFactory
    {
        public string Get() => "QuerySubject";
    }

    public class NatsReceiverCommandQueueFactory : INatsReceiverCommandQueueFactory
    {
        public KeyValuePair<string, string> Get() => new KeyValuePair<string, string>("ComandsSubject", "ComandsQueue");
    }
}