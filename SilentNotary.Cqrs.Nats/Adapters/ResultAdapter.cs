using System.Runtime.Serialization;

namespace SilentNotary.Cqrs.Nats.Adapters
{
    [DataContract]
    public class ResultAdapter
    {
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public bool IsSuccess { get; set; }
    }
}