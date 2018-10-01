using SilentNotary.EF6.Configuration.Interfaces;

namespace SilentNotary.EF6.Configuration.Interfaces
{
    public interface IEntityTypeConfigurationAdapter
    {
        void Configure(IEntityTypeConfiguration entityConfiguration,
            IAutomaticEntityTypeConfiguration configuration);
    }
}
