using System;
using KickStart.Logging;
using KickStart.NLog;
using KickStart.NLog.Logging;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for NLog.
    /// </summary>
    public static class NLogExtensions
    {

        /// <summary>
        /// Use NLog as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseNLog(this IConfigurationBuilder configurationBuilder)
        {
            return UseNLog(configurationBuilder, null);
        }


        /// <summary>
        /// Use NLog as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="configure">The configure action for NLog.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseNLog(this IConfigurationBuilder configurationBuilder, Action<global::NLog.Config.LoggingConfiguration> configure)
        {
            if (configure != null)
            {
                var configuration = global::NLog.LogManager.Configuration ?? new global::NLog.Config.LoggingConfiguration();
                configure(configuration);

                // Activate the configuration
                global::NLog.LogManager.Configuration = configuration;
            }

            // register log writer
            Logger.RegisterWriter(NLogWriter.Default);

            return configurationBuilder;
        }
    }
}
