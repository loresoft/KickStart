using KickStart.Logging;

namespace KickStart.Microsoft.Logging
{
    /// <summary>
    /// Microsoft.Extensions.Logging extension
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Use Microsoft.Extensions.Logging as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="loggerFactory">The logger factory to use.</param>
        /// <returns></returns>        
        public static IConfigurationBuilder UseLogger(this IConfigurationBuilder configurationBuilder, global::Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {

            // register log writer
            var writer = new LoggerWriter(loggerFactory);
            Logger.RegisterWriter(writer);

            return configurationBuilder;
        }

    }
}
