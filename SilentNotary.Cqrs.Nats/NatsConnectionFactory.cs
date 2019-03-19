using NATS.Client;
using SilentNotary.Cqrs.Nats.Abstract;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsConnectionFactory : INatsConnectionFactory
    {
        private readonly INatsSerializer _serializer;
        private readonly string _url;
        private IEncodedConnection _connection;

        public NatsConnectionFactory(INatsSerializer serializer, string url)
        {
            _serializer = serializer;
            _url = url;
        }

        public IEncodedConnection Get<T>()
        {
            _connection = new ConnectionFactory().CreateEncodedConnection(_url);

            _connection.OnDeserialize = _serializer.Deserialize<T>;
            _connection.OnSerialize = _serializer.Serialize<T>;

            return _connection;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}