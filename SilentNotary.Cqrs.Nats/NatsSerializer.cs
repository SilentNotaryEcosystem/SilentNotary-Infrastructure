using System;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using SilentNotary.Cqrs.Nats.Abstract;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsSerializer : INatsSerializer
    {
        public byte[] Serialize<T>(object message)
        {
            if (message == null)
                return null;
            
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, message);
                return stream.ToArray();
            }
        }

        public object Deserialize<T>(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                return serializer.ReadObject(stream);
            }
        }

        public T DeserializeMsg<T>(string command, Type cmdType = null)
        {
            return (T) JsonConvert.DeserializeObject(command, cmdType ?? typeof(T));
        }
    }
}