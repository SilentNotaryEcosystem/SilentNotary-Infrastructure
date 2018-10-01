using SilentNotary.EF6.Configuration.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SilentNotary.EF6.Configuration
{
    public class ConfigurableDbContext : DbContext
    {
        private readonly IEntityTypeConfigurationProvider _provider;
        protected string ConnectionString { get; set; }

        protected ConfigurableDbContext(IEntityTypeConfigurationProvider configurationProvider, string connString)
        {
            _provider = configurationProvider;
            ConnectionString = connString;
        }
    }
}
