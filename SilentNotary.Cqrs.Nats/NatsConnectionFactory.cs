using System;
using System.Collections.Concurrent;
using NATS.Client;
using SilentNotary.Cqrs.Nats.Abstract;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsConnectionFactory : INatsConnectionFactory
    {
        private readonly INatsSerializer _serializer;
        public readonly Options Options;

        private ConcurrentDictionary<Type, IEncodedConnection> _connections =
            new ConcurrentDictionary<Type, IEncodedConnection>();

        public NatsConnectionFactory(INatsSerializer serializer, Options options)
        {
            _serializer = serializer;
            Options = options;
        }

        public IEncodedConnection Get<T>()
        {
            var connection = GetConnection<T>();
            connection.OnDeserialize = _serializer.Deserialize<T>;
            connection.OnSerialize = _serializer.Serialize<T>;

            return connection;
        }

        public void Dispose()
        {
            foreach (var connection in _connections)
            {
                connection.Value.Dispose();
            }
        }

        private IEncodedConnection GetConnection<T>()
        {
            var type = typeof(T);

            if (_connections.TryGetValue(type, out var connection))
                return connection;

            try
            {
                connection = new ConnectionFactory().CreateEncodedConnection(Options);
            }
            catch (NATSConnectionException ex)
            {
                throw new Exception($"Nats connection error: {ex.Message}");
            }
            catch (NATSNoServersException ex)
            {
                throw new Exception($"Nats no server error: {ex.Message}");
            }

            while (!_connections.TryAdd(type, connection));

            return connection;
        }
    }
}