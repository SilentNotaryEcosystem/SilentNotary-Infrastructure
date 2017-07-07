using System.Data.Entity;
using SmartDotNet.EF6.Configuration.Extensions;
using SmartDotNet.EF6.Configuration.Interfaces;

namespace SmartDotNet.EF6.Configuration
{
    public class ConfigurableDbContext : DbContext
    {
        private readonly IEntityTypeConfigurationProvider _provider;

        protected ConfigurableDbContext(IEntityTypeConfigurationProvider configurationProvider, string connStringName)
            : base(connStringName)
        {
            _provider = configurationProvider;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DbModelBuilderExtensions.AddExplicitConfigurations(modelBuilder, _provider);
            base.OnModelCreating(modelBuilder);
        }

    }
}
