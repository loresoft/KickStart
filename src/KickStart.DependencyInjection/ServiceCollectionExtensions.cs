using System;
using KickStart;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure and run the KickStart extensions.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurator">The <see langword="delegate"/> to configure KickStart before execution of the extensions.</param>
        /// <example>Configure KickStart to use startup tasks.
        /// <code><![CDATA[
        /// services.KickStart(config => config
        ///     .IncludeAssemblyFor<Startup>()
        ///     .UseStartupTask()
        /// );]]></code>
        /// </example>
        public static IServiceCollection KickStart(this IServiceCollection services, Action<IConfigurationBuilder> configurator)
        {
            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger(typeof(Kick));

            Kick.Start(builder =>
            {
                builder
                    .LogTo(m => logger?.LogDebug(m))
                    .UseDependencyInjection(d => d.Creator(() => services));

                configurator?.Invoke(builder);
            });

            return services;
        }
    }
}
