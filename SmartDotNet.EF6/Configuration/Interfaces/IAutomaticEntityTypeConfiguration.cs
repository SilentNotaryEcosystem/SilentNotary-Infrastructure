using System.Data.Entity.ModelConfiguration.Configuration;

namespace SmartDotNet.EF6.Configuration.Interfaces
{
    public interface IAutomaticEntityTypeConfiguration
    {
        void AddEntityConfiguration(ConfigurationRegistrar registrar);
    }
}
