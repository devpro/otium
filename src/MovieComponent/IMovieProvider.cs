using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otium.MovieComponent
{
    public interface IMovieProvider
    {
        Task<List<MovieModel>> FindAsync(string title, string year);
    }
}
