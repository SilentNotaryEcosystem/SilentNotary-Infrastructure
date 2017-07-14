using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using In.Di;
using Newtonsoft.Json.Linq;

namespace In.Cqrs
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

        public async Task<Result> SendAsync<TInput>(TInput command) where TInput : IMessage
        {
            var messageResult = GetLogModel(command);

            try
            {
                var handler = _diScope.Resolve<IMsgHandler<TInput>>();
                return await handler.Handle(command);
            }
            catch (Exception e)
            {
                messageResult.Socceed = false;
                messageResult.Info = e.ToString();
                throw;
            }
            finally
            {
                SaveCommand(messageResult);
            }
        }

        public async Task<Result<TOutput>> SendAsync<TInput, TOutput>(TInput command) where TInput : IMessage
        {
            var messageResult = GetLogModel(command);

            try
            {
                var handler = _diScope.Resolve<IMsgHandler<TInput, TOutput>>();
                return await handler.Handle(command);
            }
            catch (Exception e)
            {
                messageResult.Socceed = false;
                messageResult.Info = e.ToString();
                throw;
            }
            finally
            {
                SaveCommand(messageResult);
            }
        }

        private void SaveCommand(IMessageResult msgResult)
        {
            _storage.Add(msgResult);
            _storage.Save(msgResult);
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
