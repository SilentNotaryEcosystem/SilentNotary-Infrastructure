using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using SmartDotNet.EF6.Configuration.Interfaces;

namespace SmartDotNet.EF6.Configuration
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