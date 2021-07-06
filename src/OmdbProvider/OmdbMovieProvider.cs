using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Otium.MovieComponent;
using Otium.OmdbProvider.Entities;

namespace Otium.OmdbProvider
{
    public class OmdbMovieProvider : IMovieProvider
    {
        private readonly IMapper _mapper;

        private readonly OmdbHttpClient _httpClient;

        public OmdbMovieProvider(IMapper mapper, OmdbHttpClient httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<MovieModel> FindOneAsync(string id = null, string title = null, string year = null)
        {
            var entity = await _httpClient.FindOneAsync<Movie>(EntityType.Movie, id, title, year);
            return _mapper.Map<MovieModel>(entity);
        }

        public async Task<List<MovieModel>> FindAsync(string search, string year = null)
        {
            var entities = await _httpClient.FindAsync(EntityType.Movie, search, year);
            return _mapper.Map<List<MovieModel>>(entities);
        }
    }
}
