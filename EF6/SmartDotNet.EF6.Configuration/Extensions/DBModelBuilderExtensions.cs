using System;
using System.Data.Entity;
using System.Linq;
using SmartDotNet.EF6.Configuration.Interfaces;

namespace SmartDotNet.EF6.Configuration.Extensions
{
    public static class DbModelBuilderExtensions
    {
        public static void AddExplicitConfigurations(this DbModelBuilder builder, 
            IEntityTypeConfigurationProvider configProvider)
        {            
            var baseInterfaceType = typeof(IEntityTypeConfiguration);

            foreach (var config in configProvider.GetConfigurations())
            {
                var entityType = config.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && baseInterfaceType.IsAssignableFrom(i))
                    .Select(i => i.GetGenericArguments().First())
                    .First();

                ApplyConfiguration(builder, entityType, config);                
            }
        }

        private static void ApplyConfiguration(this DbModelBuilder builder, Type entityType, IEntityTypeConfiguration config)
        {
            var adapterType = typeof(EntityTypeConfigurationAdapter<>)
                .MakeGenericType(entityType);
            var autoConfigType = typeof(AutomaticEntityTypeConfiguration<>)
                .MakeGenericType(entityType);

            var adapter = (IEntityTypeConfigurationAdapter) Activator.CreateInstance(adapterType);
            var autoConfiguration = (IAutomaticEntityTypeConfiguration) Activator.CreateInstance(autoConfigType);

            adapter.Configure(config, autoConfiguration);
            autoConfiguration.AddEntityConfiguration(builder.Configurations);
        }
    }
}
