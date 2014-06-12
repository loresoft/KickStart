using System;
using KickStart.Autofac;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for Autofac.
    /// </summary>
    public static class AutofacExtensions
    {
        /// <summary>
        /// Use the KickStart extension to configure Autofac.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure KickStart.
        /// </returns>
        public static IConfigurationBuilder UseAutofac(this IConfigurationBuilder configurationBuilder)
        {
            return UseAutofac(configurationBuilder, null);
        }

        /// <summary>
        /// Use the KickStart extension to configure Autofac.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="configure">The <see langword="delegate"/> to configure Autofac options.</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to configure KickStart.
        /// </returns>
        public static IConfigurationBuilder UseAutofac(this IConfigurationBuilder configurationBuilder, Action<IAutofacBuilder> configure)
        {
            var options = new AutofacOptions();
            var service = new AutofacStarter(options);

            if (configure != null)
            {
                var builder = new AutofacBuilder(options);
                configure(builder);
            }

            configurationBuilder.ExcludeName("Autofac");
            configurationBuilder.Use(service);

            return configurationBuilder;
        }

    }
}