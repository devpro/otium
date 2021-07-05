using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Otium.OmdbProvider
{
    public static class OmdbServiceCollectionExtensions
    {
        /// <summary>
        /// Add the dependency injection configuration.
        /// </summary>
        /// <typeparam name="T">Instance of <see cref="IOmdbConfiguration"/></typeparam>
        /// <param name="services">Collection of services that will be completed</param>
        /// <returns></returns>
        public static IServiceCollection AddOmdbClient<T>(this IServiceCollection services, T configuration)
            where T : class, IOmdbConfiguration
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.TryAddSingleton<IOmdbConfiguration>(configuration);
            services.TryAddTransient<IOmdbHttpClient, OmdbHttpClient>();
            services.TryAddTransient<MovieComponent.IMovieProvider, OmdbMovieProvider>();

            services
                .AddHttpClient(configuration.HttpClientName, client =>
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            return services;
        }
    }
}
