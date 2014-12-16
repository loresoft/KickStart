using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KickStart.Log4Net
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
            configurationBuilder.LogTo(Log4NetWriter.WriteLog);

            return configurationBuilder;
        }
    }
}
