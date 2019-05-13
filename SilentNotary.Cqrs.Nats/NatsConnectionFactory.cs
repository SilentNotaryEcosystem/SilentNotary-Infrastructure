using System;
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

        public IEncodedConnection Get<T>(Options options = null)
        {
            options = options ?? ConnectionFactory.GetDefaultOptions();
            options.Url = options.Url ?? _url;
            options.Timeout = options.Timeout <= 0 ? 60000 : options.Timeout;

            try
            {
                _connection = new ConnectionFactory().CreateEncodedConnection(options);
            }
            catch (NATSConnectionException ex)
            {
                throw new Exception($"Nats connection error: {ex.Message}");
            }
            catch (NATSNoServersException ex)
            {
                throw new Exception($"Nats no server error: {ex.Message}");
            }

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