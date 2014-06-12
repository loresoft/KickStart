using System;
using KickStart.SimpleInjector;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for SimpleInjector.
    /// </summary>
    public static class SimpleInjectorExtensions
    {
        /// <summary>
        /// Use the KickStart extension to configure SimpleInjector.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public static IConfigurationBuilder UseSimpleInjector(this IConfigurationBuilder configurationBuilder)
        {
            return UseSimpleInjector(configurationBuilder, null);
        }

        /// <summary>
        /// Use the KickStart extension to configure SimpleInjector.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="configure">The <see langword="delegate"/> to configure SimpleInjector options.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public static IConfigurationBuilder UseSimpleInjector(this IConfigurationBuilder configurationBuilder, Action<ISimpleInjectorBuilder> configure)
        {
            var options = new SimpleInjectorOptions();
            var starter = new SimpleInjectorStarter(options);

            if (configure != null)
            {
                var builder = new SimpleInjectorBuilder(options);
                configure(builder);
            }

            configurationBuilder.ExcludeName("SimpleInjector");
            configurationBuilder.Use(starter);

            return configurationBuilder;
        }
    }
}