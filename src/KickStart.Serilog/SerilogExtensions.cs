using System;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for Serilog.
    /// </summary>
    public static class SerilogExtensions
    {
        /// <summary>
        /// Use Serilog as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseSerilog(this IConfigurationBuilder configurationBuilder) {
            return UseSerilog(configurationBuilder, null);
        }


        /// <summary>
        /// Use Serilog as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="configure">The configure action for Serilog.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseSerilog(this IConfigurationBuilder configurationBuilder, Action<Serilog.LoggerConfiguration> configure) {
            if (configure != null) {
                var configuration = new Serilog.LoggerConfiguration();
                configure(configuration);

                // Activate the configuration
                Serilog.Log.Logger = configuration.CreateLogger();
            }

            // register log writer
            configurationBuilder.LogTo(Serilog.Log.Debug);

            return configurationBuilder;
        }

    }
}
