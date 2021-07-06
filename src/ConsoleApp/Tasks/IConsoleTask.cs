using System.Threading.Tasks;

namespace Otium.ConsoleApp.Tasks
{
    public interface IConsoleTask
    {
        Task<string> ExecuteAsync(CommandLineOptions options);
    }
}
