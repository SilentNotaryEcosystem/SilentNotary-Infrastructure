using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using SmartDotNet.Cqrs.Domain;
using SmartDotNet.Cqrs.Domain.Interfaces;

namespace SmartDotNet.Cqrs.Commands
{
    public abstract class CommandHandlerBase<TAggregate, TKey>
        where TAggregate: Aggregate<TKey>
    {
        protected readonly IDddUnitOfWork UnitOfWork;
        protected readonly AggregateRepository<TAggregate, TKey> Repository;

        public CommandHandlerBase(IDddUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = UnitOfWork.Repository<TAggregate, TKey>();
        }

        protected Task<Result> SaveChanges(TAggregate aggregate)
        {
            Repository.Update(aggregate);
            return UnitOfWork.CommitAsync();
        }

        protected Task<Result> SaveChanges(IEnumerable<TAggregate> aggregates)
        {
            foreach (var aggregate in aggregates)
                Repository.Update(aggregate);
            return UnitOfWork.CommitAsync();

        }
    }
}
