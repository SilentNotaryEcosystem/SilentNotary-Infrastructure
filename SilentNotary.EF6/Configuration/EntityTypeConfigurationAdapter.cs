using SilentNotary.EF6.Configuration.Interfaces;
using System.Data.Entity.ModelConfiguration;

namespace SilentNotary.EF6.Configuration
{
    public class EntityTypeConfigurationAdapter<TEntity>
        : IEntityTypeConfigurationAdapter where TEntity : class 
    {
        public void Configure(IEntityTypeConfiguration entityConfiguration, IAutomaticEntityTypeConfiguration configuration)
        {
            var typedEntityConfigurationInterface = (IEntityTypeConfiguration<TEntity>) entityConfiguration;
            typedEntityConfigurationInterface.ConfigureEntity((EntityTypeConfiguration<TEntity>)configuration);
        }
    }
}
