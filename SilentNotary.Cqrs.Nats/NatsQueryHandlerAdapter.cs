using System.Threading.Tasks;
using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;
using SilentNotary.Cqrs.Nats.Abstract;
using SilentNotary.Cqrs.Nats.Adapters;
using SilentNotary.Cqrs.Queries;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsQueryHandlerAdapter<TCriterion, TResult> : IQuery<TCriterion, TResult> where TCriterion : ICriterion
    {
        private readonly INatsConnectionFactory _connectionFactory;
        private readonly INatsSerializer _serializer;
        private readonly ITypeFactory _typeFactory;
        private readonly INatsSenderQueueFactory _queueFactory;

        public NatsQueryHandlerAdapter(INatsConnectionFactory connectionFactory, INatsSerializer serializer,
            ITypeFactory typeFactory, INatsSenderQueueFactory queueFactory)
        {
            _connectionFactory = connectionFactory;
            _serializer = serializer;
            _typeFactory = typeFactory;
            _queueFactory = queueFactory;
        }

        public Task<TResult> Ask(TCriterion criterion)
        {
            var connection = _connectionFactory.Get<QueryNatsAdapter>();
            
            var response = (QueryNatsAdapter) connection.Request(_queueFactory.GetQueryQueue(criterion, typeof(TResult)),
                new QueryNatsAdapter(criterion, typeof(TResult)));

            return Task.Run(() =>
                _serializer.DeserializeMsg<TResult>(response.QueryResult, _typeFactory.Get(response.QueryResultType)));
        }
    }
}