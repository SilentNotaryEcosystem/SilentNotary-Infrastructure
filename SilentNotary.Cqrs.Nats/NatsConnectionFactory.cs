using System;
using NATS.Client;
using SilentNotary.Cqrs.Nats.Abstract;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsConnectionFactory : INatsConnectionFactory
    {
        private readonly INatsSerializer _serializer;
        private readonly Options _options;
        private IEncodedConnection _connection;

        public NatsConnectionFactory(INatsSerializer serializer, Options options)
        {
            _serializer = serializer;
            _options = options;

            try
            {
                _connection = new ConnectionFactory().CreateEncodedConnection(_options);
            }
            catch (NATSConnectionException ex)
            {
                throw new Exception($"Nats connection error: {ex.Message}");
            }
            catch (NATSNoServersException ex)
            {
                throw new Exception($"Nats no server error: {ex.Message}");
            }
        }

        public IEncodedConnection Get<T>()
        {
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