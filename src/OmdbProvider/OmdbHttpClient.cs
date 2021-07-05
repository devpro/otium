using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Otium.MovieComponent;
using Otium.OmdbProvider.Entities;
using Withywoods.Net.Http;

namespace Otium.OmdbProvider
{
    /// <summary>
    /// HTTP client for OMDb REST API.
    /// </summary>
    public class OmdbHttpClient : HttpRepositoryBase, IOmdbHttpClient
    {
        private readonly IOmdbConfiguration _configuration;

        public OmdbHttpClient(
            ILogger<OmdbHttpClient> logger,
            IHttpClientFactory httpClientFactory,
            IOmdbConfiguration configuration)
            : base(logger, httpClientFactory)
        {
            _configuration = configuration;
        }

        protected override string HttpClientName => _configuration.HttpClientName;

        public async Task<List<MovieModel>> FindAsync(string title, string year)
        {
            var queryParameters = new StringBuilder();
            queryParameters.Append($"&t={title}");
            if (!string.IsNullOrEmpty(year))
            {
                queryParameters.Append($"&y={year}");
            }
            var url = GenerateUrl(arguments: queryParameters.ToString());
            var output = await GetAsync<Movie>(url);
            // TODO: AutoMapper / what happens if several matches?
            return null;
        }

        protected string GenerateUrl(string arguments = "")
        {
            return $"{_configuration.BaseUrl}/?apikey={_configuration.ApiKey}{arguments}";
        }
    }
}
