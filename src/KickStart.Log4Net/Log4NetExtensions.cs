using System;
using KickStart.Log4Net;
using KickStart.Log4Net.Logging;
using KickStart.Logging;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for log4net.
    /// </summary>
    public static class Log4NetExtensions
    {
        /// <summary>
        /// Use log4net as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseLog4Net(this IConfigurationBuilder configurationBuilder)
        {
            // register log writer
            Logger.RegisterWriter(Log4NetWriter.Default);

            return configurationBuilder;
        }
    }
}
