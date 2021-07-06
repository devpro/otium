using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Otium.ConsoleApp.Tasks;
using Otium.OmdbProvider;

namespace Otium.ConsoleApp
{
    class Program
    {
        private const string AppsettingsFilename = "appsettings.json";

        private async static Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<CommandLineOptions>(args)
                .MapResult(
                    (CommandLineOptions opts) => RunOptionsAndReturnExitCode(opts),
                    errs => Task.FromResult(HandleParseError(errs))
                );
        }

        private async static Task<int> RunOptionsAndReturnExitCode(CommandLineOptions opts)
        {
            var configuration = LoadConfiguration();
            var appConfiguration = new AppConfiguration(configuration);
            if (!appConfiguration.IsValid())
            {
                Console.WriteLine("Missing configuration. Please look at the README.");
                return -1;
            }

            using var serviceProvider = CreateServiceProvider(opts, configuration);

            var factory = new ConsoleTaskFactory(serviceProvider);

            var task = factory.Create(opts.Action, opts.Resource, out var errorMessage);
            if (task == null)
            {
                Console.WriteLine(errorMessage);
                return -1;
            }

            try
            {
                var output = await task.ExecuteAsync(opts);
                if (string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("No data returned");
                    return -1;
                }

                Console.WriteLine(output);
                return 0;
            }
            catch (Exception exc)
            {
                Console.WriteLine($"An error occured: {exc.Message}");
                return -2;
            }
        }

        private static int HandleParseError(IEnumerable<Error> errs)
        {
            var firstTag = errs.FirstOrDefault()?.Tag ?? default;
            if (firstTag == ErrorType.VersionRequestedError || firstTag == ErrorType.HelpRequestedError)
            {
                return 0;
            }

            return -2;
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                .AddJsonFile(AppsettingsFilename, true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static ServiceProvider CreateServiceProvider(CommandLineOptions opts, IConfigurationRoot configuration)
        {
            LogVerbose(opts, "Create the service provider");
            var serviceCollection = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder
                        .AddFilter("Microsoft", opts.IsVerbose ? LogLevel.Information : LogLevel.Warning)
                        .AddFilter("System", opts.IsVerbose ? LogLevel.Information : LogLevel.Warning)
                        .AddFilter("Otim", opts.IsVerbose ? LogLevel.Debug : LogLevel.Information)
                        .AddConsole();
                })
                .AddOmdbProvider(new AppConfiguration(configuration));

            ConfigureAutoMapper(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureAutoMapper(IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new OmdbMappingProfile());
                x.AllowNullCollections = true;
            });
            var mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            serviceCollection.AddSingleton(mapper);
        }

        private static void LogVerbose(CommandLineOptions opts, string message)
        {
            if (opts.IsVerbose)
            {
                Console.WriteLine(message);
            }
        }
    }
}
