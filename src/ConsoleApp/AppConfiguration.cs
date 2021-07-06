using Microsoft.Extensions.Configuration;
using Otium.OmdbProvider;

namespace Otium.ConsoleApp
{
    public class AppConfiguration : IOmdbConfiguration
    {
        private readonly IConfigurationRoot _configurationRoot;

        public AppConfiguration(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        string IOmdbConfiguration.BaseUrl => "http://www.omdbapi.com";

        string IOmdbConfiguration.ApiKey => _configurationRoot.GetSection("Omdb:ApiKey")?.Value;

        string IOmdbConfiguration.HttpClientName => nameof(OmdbHttpClient);

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(((IOmdbConfiguration)this).ApiKey);
        }
    }
}
