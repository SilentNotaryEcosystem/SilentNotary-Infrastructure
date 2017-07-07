using System.Collections.Generic;

namespace SmartDotNet.EF6.Configuration.Interfaces
{
    public interface IEntityTypeConfigurationProvider
    {
        IEnumerable<IEntityTypeConfiguration> GetConfigurations();
    }
}
