using CommandLine;

namespace Otium.ConsoleApp
{
    public class CommandLineOptions
    {
        [Value(0, MetaValue = "Action", Required = true, HelpText = "Action (possible values: \"search\", \"show\").")]
        public string Action { get; set; }

        [Value(1, MetaValue = "Resource", Required = false, HelpText = "Resource (possible values: \"movie\", \"series\", \"episode\").")]
        public string Resource { get; set; }

        [Option("id", Required = false, HelpText = "ID.")]
        public string Id { get; set; }

        [Option('t', "title", Required = false, HelpText = "Title.")]
        public string Title { get; set; }

        [Option('y', "year", Required = false, HelpText = "Year.")]
        public string Year { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool IsVerbose { get; set; }
    }
}
