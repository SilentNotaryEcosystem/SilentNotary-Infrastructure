using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using NATS.Client;
using Newtonsoft.Json.Linq;
using SilentNotary.Common;
using SilentNotary.Cqrs.Nats.Abstract;
using SilentNotary.Cqrs.Nats.Adapters;

namespace SilentNotary.Cqrs.Nats
{
    public class NatsMessageBus : IMessageSender, IDisposable
    {
        private readonly IDiScope _diScope;

        private readonly IStorage<IMessageResult> _storage;
        private readonly INatsSenderQueueFactory _queueFactory;
        private readonly IEncodedConnection _connection;
        private readonly IEncodedConnection _responseConnection;
        private List<IAsyncSubscription> _subscriptions = new List<IAsyncSubscription>();
        private string _replySubj;

        public NatsMessageBus(IDiScope diScope, INatsConnectionFactory connectionFactory,
            IStorage<IMessageResult> storage, INatsSenderQueueFactory queueFactory)
        {
            _diScope = diScope;
            _storage = storage;
            _queueFactory = queueFactory;
            _connection = connectionFactory.Get<CommandNatsAdapter>();
            _responseConnection = connectionFactory.Get<ResultAdapter>();
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
                var commandQueue = _queueFactory.GetCommandQueue(command);
                _replySubj = GetRandomString();
                _connection.Publish(commandQueue.Value, _replySubj,
                    new CommandNatsAdapter(command));
                _connection.Flush();

                return await GetResponse();
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

        public async Task<Result<TOutput>> SendAsync<TInput, TOutput>(TInput command) where TInput : IMessage
        {
            throw new NotSupportedException();
        }

        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
        {
            throw new NotImplementedException();
        }

        private Task<Result> GetResponse()
        {
            var promise = new TaskCompletionSource<Result>();
            var completed = 0;
            var waitTime = 0;

            ThreadPool.QueueUserWorkItem(data =>
            {
                while (completed == 0)
                {
                    if (waitTime >= 60)
                    {
                        promise.SetResult(Result.Fail("Nats connection timeout exceed"));
                        break;
                    }
                    
                    Thread.Sleep(1000);
                    waitTime++;
                }
            });

            var subscription = _responseConnection.SubscribeAsync(_replySubj, "responseQueue",
                (sender, args) =>
                {
                    var result = (ResultAdapter) args.ReceivedObject;
                    promise.SetResult(result.IsSuccess ? Result.Ok() : Result.Fail(result.Data));
                    completed++;
                });
            _subscriptions.Add(subscription);
            
            return promise.Task;
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

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private string GetRandomString(bool lowerCase = true, int size = 7)
        {
            var builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (var i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}