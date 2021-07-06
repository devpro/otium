using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Otium.MovieComponent;

namespace Otium.ConsoleApp.Tasks
{
    public class SearchMovieTask : TaskBase
    {
        private readonly ILogger<SearchMovieTask> _logger;

        private readonly IMovieProvider _movieProvider;

        public SearchMovieTask(ILogger<SearchMovieTask> logger, IMovieProvider movieProvider)
        {
            _logger = logger;
            _movieProvider = movieProvider;
        }

        public override async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            _logger.LogDebug("Searching for movie");

            var movies = await _movieProvider.FindAsync(options.Title, options.Year);
            if (!movies.Any())
            {
                return null;
            }

            return $"Successful query, {movies.Count} movies found = {string.Join(",", movies.Select(x => $"{x.OriginalTitle} ({x.ImdbId})"))}";
        }
    }
}
