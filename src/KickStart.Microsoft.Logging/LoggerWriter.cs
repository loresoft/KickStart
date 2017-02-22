using System;
using KickStart.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

namespace KickStart.Microsoft.Logging
{
    /// <summary>
    /// Microsoft.Extensions.Logging log writer adapter 
    /// </summary>
    public class LoggerWriter : ILogWriter
    {
        private static readonly Func<object, Exception, string> _messageFormatter = MessageFormatter;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerWriter"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory to write to</param>
        public LoggerWriter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Writes the specified LogData to NLog.
        /// </summary>
        /// <param name="logData">The log data.</param>
        public void WriteLog(LogData logData)
        {
            var logger = _loggerFactory.CreateLogger(logData.Logger);
            var level = ToLogLevel(logData.LogLevel);

            if (logData.MessageFormatter != null)
            {
                logger.Log(level, 0, logData, logData.Exception, _messageFormatter);
            }
            else
            {
                var state = new FormattedLogValues(logData.Message, logData.Parameters);
                logger.Log(level, 0, state, logData.Exception, _messageFormatter);
            }
        }

        /// <summary>
        /// Converts the LogLevel to Microsoft.Extensions.Logging.LogLevel
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns></returns>
        public static global::Microsoft.Extensions.Logging.LogLevel ToLogLevel(global::KickStart.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case global::KickStart.Logging.LogLevel.Fatal: return global::Microsoft.Extensions.Logging.LogLevel.Critical;
                case global::KickStart.Logging.LogLevel.Error: return global::Microsoft.Extensions.Logging.LogLevel.Error;
                case global::KickStart.Logging.LogLevel.Warn: return global::Microsoft.Extensions.Logging.LogLevel.Warning;
                case global::KickStart.Logging.LogLevel.Info: return global::Microsoft.Extensions.Logging.LogLevel.Information;
                case global::KickStart.Logging.LogLevel.Trace: return global::Microsoft.Extensions.Logging.LogLevel.Trace;
            }

            return global::Microsoft.Extensions.Logging.LogLevel.Debug;
        }
        
        private static string MessageFormatter(object state, Exception error)
        {
            var logData = state as LogData;
            if (logData == null)
                return state.ToString();

            return logData.FormatMessage();
        }
    }
}
