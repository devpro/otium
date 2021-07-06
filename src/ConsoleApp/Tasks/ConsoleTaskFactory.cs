using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Otium.MovieComponent;

namespace Otium.ConsoleApp.Tasks
{
    public class ConsoleTaskFactory
    {
        private readonly ServiceProvider _serviceProvider;

        public ConsoleTaskFactory(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IConsoleTask Create(string action, string resource, out string errorMessage)
        {
            errorMessage = null;
            switch (resource)
            {
                case "movie":
                    if (action == "list")
                    {
                        return new SearchMovieTask(
                            _serviceProvider.GetService<ILogger<SearchMovieTask>>(),
                            _serviceProvider.GetService<IMovieProvider>());
                    }
                    break;
                default:
                    errorMessage = $"Unknown resource \"{resource}\". Available resources: \"movie\", \"series\", \"episode\"";
                    return null;
            }
            errorMessage = $"Unknown action \"{action}\" for resource \"{resource}\"";
            return null;
        }
    }
}
