using System.Collections.Generic;

namespace Otium.MovieComponent
{
    public interface IMovieRepository
    {
        MovieModel FindOneById(string id);

        List<MovieModel> Find();

        void Create(MovieModel model);

        void Update(string id, MovieModel model);

        void Delete(string id);
    }
}
