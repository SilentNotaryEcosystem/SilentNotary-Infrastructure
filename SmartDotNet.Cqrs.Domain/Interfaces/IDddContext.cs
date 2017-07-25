using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IDddContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;//, IAggregateRoot;        
        Task<Result> CommitAsync();
        T ReAttach<T>(T entity) where T: class;
        ICollection<T> ReAttachCollection<T>(ICollection<T> source) where T : class;
    }
}
