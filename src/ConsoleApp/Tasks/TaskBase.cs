using System.Threading.Tasks;

namespace Otium.ConsoleApp.Tasks
{
    public abstract class TaskBase : IConsoleTask
    {
        public abstract Task<string> ExecuteAsync(CommandLineOptions options);
    }
}
