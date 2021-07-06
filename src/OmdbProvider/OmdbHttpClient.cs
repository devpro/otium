using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Otium.OmdbProvider.Entities;
using Withywoods.Net.Http;

namespace Otium.OmdbProvider
{
    /// <summary>
    /// HTTP client for OMDb REST API.
    /// </summary>
    public class OmdbHttpClient : HttpRepositoryBase
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

        public async Task<T> FindOneAsync<T>(
            EntityType type,
            string id = null,
            string title = null,
            string year = null)
            where T : EntityBase
        {
            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(id), "id or title must be provided while searching for one element");
            }

            var queryParameters = new StringBuilder();
            if (!string.IsNullOrEmpty(id))
            {
                queryParameters.Append($"&i={id}");
            }
            if (!string.IsNullOrEmpty(title))
            {
                queryParameters.Append($"&t={title}");
            }
            if (!string.IsNullOrEmpty(year))
            {
                queryParameters.Append($"&y={year}");
            }
            queryParameters.Append($"&type={type.ToString().ToLower()}");

            var url = GenerateUrl(arguments: queryParameters.ToString());

            return await GetAsync<T>(url);
        }

        public async Task<List<Search>> FindAsync(
            EntityType type,
            string search,
            string year)
        {
            if (string.IsNullOrEmpty(search))
            {
                throw new ArgumentNullException(nameof(search));
            }

            var queryParameters = new StringBuilder();
            queryParameters.Append($"&s={search}");
            if (!string.IsNullOrEmpty(year))
            {
                queryParameters.Append($"&y={year}");
            }
            queryParameters.Append($"&type={type.ToString().ToLower()}");

            var url = GenerateUrl(arguments: queryParameters.ToString());
            var searchResult = await GetAsync<SearchResult>(url);
            if (searchResult.Response != "True")
            {
                Logger.LogWarning($"Find OMDb returns a not \"True\" response: {searchResult.Response}");
                return null;
            }

            return searchResult.Search;
        }

        private string GenerateUrl(string arguments = "")
        {
            return $"{_configuration.BaseUrl}/?apikey={_configuration.ApiKey}{arguments}";
        }
    }
}
