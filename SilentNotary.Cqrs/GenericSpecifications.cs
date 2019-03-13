using System;
using SilentNotary.Common;
using SilentNotary.Specifications;

namespace SilentNotary.Cqrs
{
    public static class GenericSpecifications
    {
        public static Specification<TEntity> All<TEntity>()
            where TEntity : class
        {
            return new GenericSpecification<TEntity>(entity => true);
        }

        public static Specification<TEntity> WithId<TEntity>(int id)
            where TEntity : class, IHasKey<int>
        {
            return new GenericSpecification<TEntity>(entity => entity.Id.Equals(id));
        }

        public static Specification<TEntity> WithId<TEntity>(Guid id)
            where TEntity : class, IHasKey<Guid>
        {
            return new GenericSpecification<TEntity>(entity => entity.Id.Equals(id));
        }

        public static Specification<TEntity> WithId<TEntity>(string id)
            where TEntity : class, IHasKey<string>
        {
            return new GenericSpecification<TEntity>(entity => entity.Id.Equals(id));
        }
    }
}