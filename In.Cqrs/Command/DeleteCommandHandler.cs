﻿using In.Domain;
using In.Entity.Uow;

namespace In.Cqrs.Command
{
    public class DeleteCommandHandler<TEntity>
        where TEntity : class, IHasKey
    {
        private readonly IDataSetUow _dataSetUow;
        
        public DeleteCommandHandler(IDataSetUow dataSetUow)
        {
            _dataSetUow = dataSetUow;
        }
        

        public string Handle(TEntity message)
        {
            //var entity = _dataSetUow.Find<TEntity>(message);
            //if (entity == null)
            //{
            //    throw new ArgumentException($"Entity {typeof(TEntity).Name} with id={message.GetId()} doesn't exists");
            //}

            _dataSetUow.Remove(message);
            
            return string.Empty;
        }
        
    }
}
