using System;

namespace Otium.OmdbProvider.IntegrationTests
{
    public class IntegrationOmdbConfiguration : IOmdbConfiguration
    {
        public string BaseUrl => "http://www.omdbapi.com";

        public string ApiKey => Environment.GetEnvironmentVariable("Omdb__Integration__ApiKey");

        public string HttpClientName => nameof(OmdbHttpClient);
    }
}
