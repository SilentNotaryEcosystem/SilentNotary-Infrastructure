using SilentNotary.EF6.Configuration.Interfaces;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace SilentNotary.EF6.Configuration
{
    public class AutomaticEntityTypeConfiguration<TEntity> 
        : EntityTypeConfiguration<TEntity>, IAutomaticEntityTypeConfiguration where TEntity : class 
    {
        public void AddEntityConfiguration(ConfigurationRegistrar registrar)
        {
            registrar.Add(this);
        }
    }
}