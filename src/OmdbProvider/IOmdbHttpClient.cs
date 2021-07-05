using System.Collections.Generic;
using System.Threading.Tasks;
using Otium.MovieComponent;

namespace Otium.OmdbProvider
{
    public interface IOmdbHttpClient
    {
        Task<List<MovieModel>> FindAsync(string title, string year);
    }
}
