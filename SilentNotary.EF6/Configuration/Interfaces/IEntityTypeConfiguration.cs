using System.Data.Entity.ModelConfiguration;

namespace SilentNotary.EF6.Configuration.Interfaces
{
    public interface IEntityTypeConfiguration
    {

    }

    public interface IEntityTypeConfiguration<TEntity>
        : IEntityTypeConfiguration where TEntity : class
    {
        void ConfigureEntity(EntityTypeConfiguration<TEntity> configuration);
    }
}
