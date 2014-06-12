using System;
using KickStart.Ninject;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for Ninject.
    /// </summary>
    public static class NinjectExtensions
    {
        /// <summary>
        /// Use the KickStart extension to configure Ninject.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public static IConfigurationBuilder UseNinject(this IConfigurationBuilder configurationBuilder)
        {
            return UseNinject(configurationBuilder, null);
        }

        /// <summary>
        /// Use the KickStart extension to configure Ninject.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="configure">The <see langword="delegate"/> to configure Ninject options.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public static IConfigurationBuilder UseNinject(this IConfigurationBuilder configurationBuilder, Action<INinjectBuilder> configure)
        {
            var options = new NinjectOptions();
            var service = new NinjectStarter(options);

            if (configure != null)
            {
                var builder = new NinjectBuilder(options);
                configure(builder);
            }

            configurationBuilder.ExcludeName("Ninject");
            configurationBuilder.Use(service);

            return configurationBuilder;
        }

    }
}