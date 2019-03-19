using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;
using SilentNotary.Cqrs.Nats.Abstract;
using SilentNotary.Cqrs.Queries;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsQueryFactory : IQueryFactory
    {
        private readonly INatsConnectionFactory _connectionFactory;
        private readonly INatsSerializer _serializer;
        private readonly ITypeFactory _typeFactory;
        private readonly INatsQueueFactory _queueFactory;

        public NatsQueryFactory(INatsConnectionFactory connectionFactory, INatsSerializer serializer,
            ITypeFactory typeFactory, INatsQueueFactory queueFactory)
        {
            _connectionFactory = connectionFactory;
            _serializer = serializer;
            _typeFactory = typeFactory;
            _queueFactory = queueFactory;
        }

        public IQuery<TCriterion, TResult> Create<TCriterion, TResult>() where TCriterion : ICriterion
        {
            return new NatsQueryHandlerAdapter<TCriterion, TResult>(_connectionFactory, _serializer, _typeFactory, _queueFactory);
        }
    }
}