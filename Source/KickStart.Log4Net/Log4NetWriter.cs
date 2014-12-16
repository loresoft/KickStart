using System;
using log4net;

namespace KickStart.Log4Net
{
    /// <summary>
    /// log4net Log adaptor
    /// </summary>
    public static class Log4NetWriter
    {
        /// <summary>
        /// Writes the specified LogData to log4net.
        /// </summary>
        /// <param name="logData">The log data.</param>
        public static void WriteLog(LogData logData)
        {
            var name = logData.Logger ?? typeof(Log4NetWriter).FullName;

            var logger = LogManager.GetLogger(name);

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
            bool isFormatted = logData.Parameters != null && logData.Parameters.Length > 0;

            string message = isFormatted
                ? string.Format(logData.FormatProvider, logData.Message, logData.Parameters)
                : logData.Message;


            if (logData.Exception == null)
                logAction(message);
            else
                errorAction(message, logData.Exception);
        }
    }
}