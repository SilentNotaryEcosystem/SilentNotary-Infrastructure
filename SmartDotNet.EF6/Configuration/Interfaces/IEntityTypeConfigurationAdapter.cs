namespace SmartDotNet.EF6.Configuration.Interfaces
{
    public interface IEntityTypeConfigurationAdapter
    {
        void Configure(IEntityTypeConfiguration entityConfiguration,
            IAutomaticEntityTypeConfiguration configuration);
    }
}
