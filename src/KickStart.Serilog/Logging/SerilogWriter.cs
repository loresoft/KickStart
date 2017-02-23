using System;
using KickStart.Logging;
using Serilog.Events;
using Log = global::Serilog.Log;

namespace KickStart.Serilog.Logging
{
    /// <summary>
    /// Serilog log writer adapter 
    /// </summary>
    public class SerilogWriter : ILogWriter
    {
        /// <summary>
        /// Writes the specified <see cref="LogData"/> to the underlying logger.
        /// </summary>
        /// <param name="logData">The log data.</param>
        public void WriteLog(LogData logData)
        {
            var logger = Log.Logger;

            switch (logData.LogLevel)
            {
                case LogLevel.Trace:
                    if (Log.IsEnabled(LogEventLevel.Verbose))
                        WriteLog(logData, logger.Verbose);

                    break;
                case LogLevel.Info:
                    if (Log.IsEnabled(LogEventLevel.Information))
                        WriteLog(logData, logger.Information);

                    break;
                case LogLevel.Warn:
                    if (Log.IsEnabled(LogEventLevel.Warning))
                        WriteLog(logData, logger.Warning);

                    break;
                case LogLevel.Error:
                    if (Log.IsEnabled(LogEventLevel.Error))
                        WriteLog(logData, logger.Error);

                    break;
                case LogLevel.Fatal:
                    if (Log.IsEnabled(LogEventLevel.Fatal))
                        WriteLog(logData, logger.Fatal);

                    break;
                default:
                    if (Log.IsEnabled(LogEventLevel.Debug))
                        WriteLog(logData, logger.Debug);

                    break;
            }
        }


        private static void WriteLog(LogData logData, Action<Exception, string, object[]> writer)
        {
            string message = FormatMessage(logData);

            writer(logData.Exception, message, logData.Parameters);
        }

        private static string FormatMessage(LogData logData)
        {
            string message = null;
            try
            {
                message = logData.MessageFormatter != null
                    ? logData.MessageFormatter()
                    : logData.Message;
            }
            catch (Exception)
            {
                // don't throw error
            }

            return message ?? string.Empty;
        }


        private static readonly Lazy<SerilogWriter> _current = new Lazy<SerilogWriter>(() => new SerilogWriter());

        /// <summary>
        /// Gets the current singleton instance of <see cref="SerilogWriter"/>.
        /// </summary>
        /// <value>The current singleton instance.</value>
        /// <remarks>
        /// An instance of <see cref="SerilogWriter"/> wont be created until the very first 
        /// call to the sealed class. This is a CLR optimization that
        /// provides a properly lazy-loading singleton. 
        /// </remarks>
        public static SerilogWriter Default
        {
            get { return _current.Value; }
        }

    }
}
