using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Newtonsoft.Json.Linq;

namespace SilentNotary.Common
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class SimpleMsgBus : IMessageSender
    {
        private readonly IDiScope _diScope;
        private readonly IStorage<IMessageResult> _storage;

        public SimpleMsgBus(IDiScope diScope, IStorage<IMessageResult> storage)
        {
            _diScope = diScope;
            _storage = storage;
        }

        public Result Send<T>(T command) where T : IMessage
        {
            return AsyncHelpers.RunSync(() => SendAsync(command));
        }

        public async Task<Result> SendAsync(IMessage command)
        {
            //var messageResult = GetLogModel(command);

            var cmdType = command.GetType();

            var openGenericType = typeof(IMsgHandler<>);
            var closedGenericType = openGenericType.MakeGenericType(cmdType);

            var handler = _diScope.Resolve(closedGenericType);

            try
            {
                var methods = handler
                    .GetType()
                    .GetTypeInfo()
                    .GetDeclaredMethods("Handle");
                
                foreach (var method in methods)
                {
                    var contains = method.GetParameters()
                        .FirstOrDefault()
                        ?.ToString()
                        .Contains(cmdType.ToString());

                    if (contains == true)
                    {
                        return await (Task<Result>) method.Invoke(handler, new[] {command});
                    }
                }

                throw new Exception("Handler no found");
            }
            catch (Exception ex)
            {
                // messageResult.Socceed = false;
                // messageResult.Info = ex.ToString();
                throw;
            }
            // finally
            // {
            //     await SaveCommand(messageResult);
            // }
        }

        public async Task<Result> SendAsync<TInput>(TInput command) where TInput : IMessage
        {
            //var messageResult = GetLogModel(command);

            try
            {
                var handler = _diScope.Resolve<IMsgHandler<TInput>>();
                return await handler.Handle(command);
            }
            catch (Exception e)
            {
                // messageResult.Socceed = false;
                // messageResult.Info = e.ToString();
                throw;
            }
            // finally
            // {
            //     await SaveCommand(messageResult);
            // }
        }

        public async Task<Result<TOutput>> SendAsync<TInput, TOutput>(TInput command) where TInput : IMessage
        {
            //var messageResult = GetLogModel(command);

            try
            {
                var handler = _diScope.Resolve<IMsgHandler<TInput, TOutput>>();
                return await handler.Handle(command);
            }
            catch (Exception e)
            {
                //messageResult.Socceed = false;
                //messageResult.Info = e.ToString();
                throw;
            }
            // finally
            // {
            //     await SaveCommand(messageResult);
            // }
        }

        public async Task PublishAsync<TEvent>(TEvent @event)
            where TEvent : IIntegrationEvent
        {
            var handlers = _diScope.Resolve<IEnumerable<IIntegrationEventHandler<TEvent>>>().ToArray();

            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
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