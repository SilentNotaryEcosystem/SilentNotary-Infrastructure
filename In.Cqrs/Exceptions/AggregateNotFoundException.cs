using System;

namespace In.Legacy.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException()
        {
        }

        public AggregateNotFoundException(string message)
        : base(message)
        {
        }

        public AggregateNotFoundException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}