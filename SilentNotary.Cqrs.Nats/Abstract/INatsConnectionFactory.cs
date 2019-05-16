using System;
using NATS.Client;

namespace SilentNotary.Cqrs.Nats.Abstract
{
    public interface INatsConnectionFactory : IDisposable
    {
        IEncodedConnection Get<T>();
    }
}