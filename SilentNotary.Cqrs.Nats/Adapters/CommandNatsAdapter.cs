using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SilentNotary.Cqrs.Nats.Adapters
{
    [DataContract]
    public class CommandNatsAdapter
    {
        [DataMember]
        public string Command { get; set; }
        [DataMember]
        public string CommandType { get; set; }

        public CommandNatsAdapter(object command)
        {
            Command = JsonConvert.SerializeObject(command);
            CommandType = command.GetType().ToString();
        }
        
        public CommandNatsAdapter(){}
    }
}