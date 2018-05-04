using SmartDotNet.Cqrs.Domain.Interfaces;
using SmartDotNet.Specifications;

namespace SmartDotNet.Cqrs
{
    public static class GenericSpecifications
    {
        public static Specification<TEntity> All<TEntity>()
        {
            return new GenericSpecification<TEntity>(entity => true);
        }

        public static Specification<TEntity> WithId<TEntity, TKey>(TKey id)
            where TEntity : IHasKey<TKey>
        {
            return new GenericSpecification<TEntity>(entity => entity.Id.Equals(id));
        }
    }
}
