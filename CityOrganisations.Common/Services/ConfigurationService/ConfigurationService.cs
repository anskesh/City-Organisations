using System.IO;
using Microsoft.Extensions.Configuration;

namespace CityOrganisations.Configuration
{
    public class ConfigurationService
    {
        public Configuration Configuration;
        
        private string _configurationPath;
        
        public ConfigurationService(string configurationPath)
        {
            _configurationPath = configurationPath;
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configurationPath)
                .Build();

            Configuration = new Configuration()
            {
                DataBasePath = configuration.GetConnectionString("Database")
            };
        }
    }
}