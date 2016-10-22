using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KickStart.Logging;
using KickStart.Serilog.Logging;

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
        public static IConfigurationBuilder UseSerilog(this IConfigurationBuilder configurationBuilder, Action<global::Serilog.LoggerConfiguration> configure) {
            if (configure != null) {
                var configuration = new global::Serilog.LoggerConfiguration();
                configure(configuration);

                // Activate the configuration
                global::Serilog.Log.Logger = configuration.CreateLogger();
            }

            // register log writer
            Logger.RegisterWriter(SerilogWriter.Default);

            return configurationBuilder;
        }

    }
}
