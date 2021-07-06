using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Otium.MovieComponent;
using Xunit;

namespace Otium.OmdbProvider.IntegrationTests
{
    public class OmdbMovieProviderTest
    {
        private readonly IOmdbConfiguration _configuration;

        public OmdbMovieProviderTest()
        {
            _configuration = new IntegrationOmdbConfiguration();

            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new OmdbMappingProfile());
                x.AllowNullCollections = true;
            });

            var services = new ServiceCollection()
                .AddLogging()
                .AddSingleton(mappingConfig.CreateMapper())
                .AddOmdbProvider(_configuration);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected ServiceProvider ServiceProvider { get; private set; }

        [Fact]
        public async Task OmdbMovieProviderFindOneAsync_ReturnData()
        {
            // Arrange
            var provider = CreateProvider();

            // Act
            var output1 = await provider.FindOneAsync(title: "Terminator", year: "1984");
            var output2 = await provider.FindOneAsync(id: "tt0088247");

            // Assert
            output1.Should().NotBeNull();
            output1.ImdbId.Should().NotBeNullOrEmpty();
            output1.Should().BeEquivalentTo(output2);
        }

        [Fact]
        public async Task OmdbMovieProviderFindAsync_ReturnData()
        {
            // Arrange
            var provider = CreateProvider();

            // Act
            var output = await provider.FindAsync(search: "Terminator");

            // Assert
            output.Should().NotBeNullOrEmpty();
            output.Should().HaveCount(10);
        }

        private IMovieProvider CreateProvider()
        {
            var logger = ServiceProvider.GetService<ILogger<OmdbHttpClient>>();
            var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>();
            var mapper = ServiceProvider.GetService<IMapper>();
            var client = new OmdbHttpClient(logger, httpClientFactory, _configuration);
            return new OmdbMovieProvider(mapper, client);
        }
    }
}
