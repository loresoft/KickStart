using System;
using KickStart.Logging;

namespace KickStart.Log4Net.Logging
{
    /// <summary>
    /// log4net Log adapter
    /// </summary>
    public class Log4NetWriter : ILogWriter
    {
        /// <summary>
        /// Writes the specified LogData to log4net.
        /// </summary>
        /// <param name="logData">The log data.</param>
        public void WriteLog(LogData logData)
        {
            var name = logData.Logger ?? typeof(Log4NetWriter).FullName;

            var logger = log4net.LogManager.GetLogger(name);

            switch (logData.LogLevel)
            {
                case LogLevel.Info:
                    if (logger.IsInfoEnabled)
                        WriteLog(logData, logger.Info, logger.Info);

                    break;
                case LogLevel.Warn:
                    if (logger.IsWarnEnabled)
                        WriteLog(logData, logger.Warn, logger.Warn);

                    break;
                case LogLevel.Error:
                    if (logger.IsErrorEnabled)
                        WriteLog(logData, logger.Error, logger.Error);

                    break;
                case LogLevel.Fatal:
                    if (logger.IsFatalEnabled)
                        WriteLog(logData, logger.Fatal, logger.Fatal);

                    break;
                default:
                    if (logger.IsDebugEnabled)
                        WriteLog(logData, logger.Debug, logger.Debug);

                    break;
            }

        }

        private static void WriteLog(LogData logData, Action<object> logAction, Action<object, Exception> errorAction)
        {
            string message = logData.FormatMessage();

            if (logData.Exception == null)
                logAction(message);
            else
                errorAction(message, logData.Exception);
        }


        private static readonly Lazy<Log4NetWriter> _current = new Lazy<Log4NetWriter>(() => new Log4NetWriter());

        /// <summary>
        /// Gets the current singleton instance of <see cref="Log4NetWriter"/>.
        /// </summary>
        /// <value>The current singleton instance.</value>
        /// <remarks>
        /// An instance of <see cref="Log4NetWriter"/> wont be created until the very first 
        /// call to the sealed class. This is a CLR optimization that
        /// provides a properly lazy-loading singleton. 
        /// </remarks>
        public static Log4NetWriter Default
        {
            get { return _current.Value; }
        }

    }
}
