using System.Data.Entity.ModelConfiguration.Configuration;

namespace SilentNotary.EF6.Configuration.Interfaces
{
    public interface IAutomaticEntityTypeConfiguration
    {
        void AddEntityConfiguration(ConfigurationRegistrar registrar);
    }
}
