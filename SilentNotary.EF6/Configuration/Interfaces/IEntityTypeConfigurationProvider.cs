using SilentNotary.EF6.Configuration.Interfaces;
using System.Collections.Generic;

namespace SilentNotary.EF6.Configuration.Interfaces
{
    public interface IEntityTypeConfigurationProvider
    {
        IEnumerable<IEntityTypeConfiguration> GetConfigurations();
    }
}
