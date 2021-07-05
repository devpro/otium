namespace Otium.OmdbProvider
{
    public interface IOmdbConfiguration
    {
        /// <summary>
        /// OMDb base URL.
        /// </summary>
        /// <example>http://www.omdbapi.com</example>
        string BaseUrl { get; }

        /// <summary>
        /// OMDb API key.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// HTTP client name.
        /// Must be unique to an application.
        /// </summary>
        string HttpClientName { get; }
    }
}
