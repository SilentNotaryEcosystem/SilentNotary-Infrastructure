using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Newtonsoft.Json.Linq;
using SilentNotary.Common;
using SilentNotary.Cqrs.Nats.Abstract;
using SilentNotary.Cqrs.Nats.Adapters;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsMessageBus : IMessageSender
    {
        private readonly IDiScope _diScope;

        private readonly IStorage<IMessageResult> _storage;
        private readonly INatsQueueFactory _queueFactory;
        private readonly INatsConnectionFactory _connectionFactory;

        public NatsMessageBus(IDiScope diScope, INatsConnectionFactory connectionFactory,
            IStorage<IMessageResult> storage, INatsQueueFactory queueFactory)
        {
            _diScope = diScope;
            _storage = storage;
            _queueFactory = queueFactory;
            _connectionFactory = connectionFactory;
        }

        public Result Send<T>(T command) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public Task<Result> SendAsync(IMessage command)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> SendAsync<TInput>(TInput command) where TInput : IMessage
        {
            var messageResult = GetLogModel(command);
            try
            {
                using (var connection = _connectionFactory.Get<CommandNatsAdapter>())
                {
                    var commandQueue = _queueFactory.GetCommandQueue(command);
                    connection.Publish(commandQueue.Key, commandQueue.Value,
                        new CommandNatsAdapter(command));
                    connection.Flush();
                }

                return Result.Ok();
            }
            catch (Exception e)
            {
                messageResult.Socceed = false;
                messageResult.Info = e.ToString();
                throw;
            }
            finally
            {
                await SaveCommand(messageResult);
            }
        }

        public Task<Result<TOutput>> SendAsync<TInput, TOutput>(TInput command) where TInput : IMessage
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
        {
            throw new NotImplementedException();
        }

        private Task SaveCommand(IMessageResult msgResult)
        {
            _storage.Add(msgResult);
            return _storage.Save(msgResult);
        }

        private IMessageResult GetLogModel(IMessage command)
        {
            var msgResult = _diScope.Resolve<IMessageResult>();
            msgResult.Body = JObject.FromObject(command).ToString();
            msgResult.Type = command.GetType().ToString();
            msgResult.Socceed = true;

            return msgResult;
        }
    }
}