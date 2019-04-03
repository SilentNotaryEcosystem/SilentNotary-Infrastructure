using System;
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
     
             public NatsMessageBus(IDiScope diScope, INatsConnectionFactory connectionFactory,
                 IStorage<IMessageResult> storage, INatsSenderQueueFactory queueFactory)
             {
                 _diScope = diScope;
                 _storage = storage;
                 _queueFactory = queueFactory;
                 _connection = connectionFactory.Get<CommandNatsAdapter>();
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
                     _connection.Publish(commandQueue.Value, Guid.NewGuid().ToString(),
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
                 var messageResult = GetLogModel(command);
                 try
                 {
                     var commandQueue = _queueFactory.GetCommandQueue(command);
                     _connection.Publish(commandQueue.Value, commandQueue.Value,
                         new CommandNatsAdapter(command));
                     _connection.Flush();
     
                     return await GetResponse<TOutput>();
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
     
             public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
             {
                 throw new NotImplementedException();
             }
     
             private Task<Result> GetResponse()
             {
                 var promise = new TaskCompletionSource<Result>();
                 _connection.SubscribeAsync("responseSubj", "responseQueue",
                     (sender, args) => { promise.SetResult((Result) args.ReceivedObject); });
                 return promise.Task;
             }
     
             private Task<Result<TResult>> GetResponse<TResult>()
             {
                 var promise = new TaskCompletionSource<Result<TResult>>();
                 _connection.SubscribeAsync("responseSubj", "responseQueue",
                     (sender, args) => { promise.SetResult((Result<TResult>) args.ReceivedObject); });
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
                 _connection.Dispose();
             }
         }
}