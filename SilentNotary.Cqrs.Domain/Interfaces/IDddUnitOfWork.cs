﻿using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SilentNotary.Cqrs.Domain.Interfaces
{
    public interface IDddUnitOfWork
    {
        AggregateRepository<TAggregate, TId> Repository<TAggregate, TId>()
            where TAggregate : Aggregate<TId>;

        IValueObjectProvider ValueObjectProvider { get; }

        Task<Result> CommitAsync();
    }
}
