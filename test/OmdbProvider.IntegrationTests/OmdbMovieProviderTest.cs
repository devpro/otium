using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Otium.OmdbProvider.IntegrationTests
{
    public class OmdbMovieProviderTest
    {
        private readonly IOmdbConfiguration _configuration;

        public OmdbMovieProviderTest()
        {
            _configuration = new IntegrationOmdbConfiguration();

            var services = new ServiceCollection()
                .AddLogging()
                .AddOmdbClient(_configuration);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected ServiceProvider ServiceProvider { get; private set; }

        [Fact]
        public async Task OmdbMovieProviderFindAsync_ReturnData()
        {
            // Arrange
            var logger = ServiceProvider.GetService<ILogger<OmdbHttpClient>>();
            var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>();
            var client = new OmdbHttpClient(logger, httpClientFactory, _configuration);
            var provider = new OmdbMovieProvider(client);

            // Act
            var output = await provider.FindAsync(title: "Terminator");

            // Assert
            output.Should().NotBeNullOrEmpty();
        }
    }
}
