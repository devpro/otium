using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otium.MovieComponent
{
    public interface IMovieProvider
    {
        Task<MovieModel> FindOneAsync(string id = null, string title = null, string year = null);

        Task<List<MovieModel>> FindAsync(string search, string year = null);
    }
}
