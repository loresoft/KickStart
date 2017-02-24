using System;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for log4net.
    /// </summary>
    public static class Log4NetExtensions
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("KickStart");

        /// <summary>
        /// Use log4net as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseLog4Net(this IConfigurationBuilder configurationBuilder)
        {
            return UseLog4Net(configurationBuilder, null);
        }

        /// <summary>
        /// Use log4net as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="configure">The configure action for log4net.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseLog4Net(this IConfigurationBuilder configurationBuilder, Action configure)
        {

            configure?.Invoke();

            // register log writer
            configurationBuilder.LogTo(_logger.Debug);

            return configurationBuilder;
        }
    }
}
