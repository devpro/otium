using System.Collections.Generic;
using System.Threading.Tasks;
using Otium.MovieComponent;

namespace Otium.OmdbProvider
{
    public class OmdbMovieProvider : IMovieProvider
    {
        private readonly OmdbHttpClient _httpClient;

        public OmdbMovieProvider(OmdbHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MovieModel>> FindAsync(string title, string year = null)
        {
            return await _httpClient.FindAsync(title, year);
        }
    }
}
