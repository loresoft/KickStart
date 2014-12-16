using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace KickStart
{
    /// <summary>
    /// Defines available log levels.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>Trace log level.</summary>
        Trace = 0,
        /// <summary>Debug log level.</summary>
        Debug = 1,
        /// <summary>Info log level.</summary>
        Info = 2,
        /// <summary>Warn log level.</summary>
        Warn = 3,
        /// <summary>Error log level.</summary>
        Error = 4,
        /// <summary>Fatal log level.</summary>
        Fatal = 5,
    }

    /// <summary>
    /// A simple logging class
    /// </summary>
    public sealed class Logger
    {
        private static Action<LogData> _logWriter = DebugWrite;
        private static readonly object _writerLock = new object();

        /// <summary>
        /// Start a fluent <see cref="LogLevel.Trace"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Trace()
        {
            var writer = ResolveWriter();
            return new LogBuilder(LogLevel.Trace, writer);
        }

        /// <summary>
        /// Start a fluent <see cref="LogLevel.Debug"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Debug()
        {
            var writer = ResolveWriter();
            return new LogBuilder(LogLevel.Debug, writer);
        }

        /// <summary>
        /// Start a fluent <see cref="LogLevel.Info"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Info()
        {
            var writer = ResolveWriter();
            return new LogBuilder(LogLevel.Info, writer);
        }

        /// <summary>
        /// Start a fluent <see cref="LogLevel.Warn"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Warn()
        {
            var writer = ResolveWriter();
            return new LogBuilder(LogLevel.Warn, writer);
        }

        /// <summary>
        /// Start a fluent <see cref="LogLevel.Error"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Error()
        {
            var writer = ResolveWriter();
            return new LogBuilder(LogLevel.Error, writer);
        }

        /// <summary>
        /// Start a fluent <see cref="LogLevel.Fatal"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Fatal()
        {
            var writer = ResolveWriter();
            return new LogBuilder(LogLevel.Fatal, writer);
        }

        /// <summary>
        /// Registers a <see langword="delegate"/> to write logs to.
        /// </summary>
        /// <param name="writer">The <see langword="delegate"/> to write logs to.</param>
        public static void RegisterWriter(Action<LogData> writer)
        {
            lock (_writerLock)
                _logWriter = writer;
        }

        private static Action<LogData> ResolveWriter()
        {
            lock (_writerLock)
                return _logWriter;
        }

        private static void DebugWrite(LogData logData)
        {
            System.Diagnostics.Debug.WriteLine((object) logData);
        }
    }

    /// <summary>
    /// A class holding log data before being written.
    /// </summary>
    public sealed class LogData
    {
        /// <summary>
        /// Gets or sets the logger name.
        /// </summary>
        /// <value>
        /// The logger name.
        /// </value>
        public string Logger { get; set; }

        /// <summary>
        /// Gets or sets the trace level.
        /// </summary>
        /// <value>
        /// The trace level.
        /// </value>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the message parameters. Used with String.Format.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public object[] Parameters { get; set; }

        /// <summary>
        /// Gets or sets the format provider.
        /// </summary>
        /// <value>
        /// The format provider.
        /// </value>
        public IFormatProvider FormatProvider { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.
        /// </value>
        public string MemberName { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the log properties.
        /// </summary>
        /// <value>
        /// The log properties.
        /// </value>
        public IDictionary<string, object> Properties { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var message = new StringBuilder();
            message
                .Append(DateTime.Now.ToString("HH:mm:ss.fff"))
                .Append(" [")
                .Append(LogLevel.ToString()[0])
                .Append("] ");

            if (!string.IsNullOrEmpty(FilePath) && !string.IsNullOrEmpty(MemberName))
            {
                message
                    .Append("[")
                    .Append(GetFileName(FilePath))
                    .Append(" ")
                    .Append(MemberName)
                    .Append("()")
                    .Append(" Ln: ")
                    .Append(LineNumber)
                    .Append("] ");
            }

            if (Parameters != null && Parameters.Length > 0)
                message.AppendFormat(FormatProvider, Message, Parameters);
            else
                message.Append(Message);

            if (Exception != null)
                message.Append(" ").Append(Exception);

            return message.ToString();
        }

        private string GetFileName(string filePath)
        {
            var parts = filePath.Split('\\', '/');
            return parts.LastOrDefault();
        }
    }

    /// <summary>
    /// A fluent <see langword="interface"/> to build log messages.
    /// </summary>
    public sealed class LogBuilder
    {
        private readonly LogData _data;
        private readonly Action<LogData> _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="KickStart.LogBuilder" /> class.
        /// </summary>
        /// <param name="logLevel">The starting trace level.</param>
        /// <param name="writer">The delegate to write logs to.</param>
        /// <exception cref="System.ArgumentNullException">writer</exception>
        public LogBuilder(LogLevel logLevel, Action<LogData> writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            _writer = writer;
            _data = new LogData();
            _data.LogLevel = logLevel;
            _data.FormatProvider = CultureInfo.InvariantCulture;
            _data.Logger = typeof(Logger).FullName;
        }

        /// <summary>
        /// Gets the log data that is being built.
        /// </summary>
        /// <value>
        /// The log data.
        /// </value>
        public LogData LogData
        {
            get { return _data; }
        }

        /// <summary>
        /// Sets the level of the logging event.
        /// </summary>
        /// <param name="logLevel">The level of the logging event.</param>
        /// <returns></returns>
        public LogBuilder Level(LogLevel logLevel)
        {
            _data.LogLevel = logLevel;
            return this;
        }

        /// <summary>
        /// Sets the logger for the logging event.
        /// </summary>
        /// <param name="logger">The name of the logger.</param>
        /// <returns></returns>
        public LogBuilder Logger(string logger)
        {
            _data.Logger = logger;

            return this;
        }

        /// <summary>
        /// Sets the logger name using the generic type.
        /// </summary>
        /// <typeparam name="TLogger">The type of the logger.</typeparam>
        /// <returns></returns>
        public LogBuilder Logger<TLogger>()
        {
            _data.Logger = typeof(TLogger).FullName;

            return this;
        }

        /// <summary>
        /// Sets the log message on the logging event.
        /// </summary>
        /// <param name="message">The log message for the logging event.</param>
        /// <returns></returns>
        public LogBuilder Message(string message)
        {
            _data.Message = message;

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, object arg0)
        {
            _data.Message = format;
            _data.Parameters = new[] { arg0 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, object arg0, object arg1)
        {
            _data.Message = format;
            _data.Parameters = new[] { arg0, arg1 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, object arg0, object arg1, object arg2)
        {
            _data.Message = format;
            _data.Parameters = new[] { arg0, arg1, arg2 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <param name="arg3">The fourth object to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, object arg0, object arg1, object arg2, object arg3)
        {
            _data.Message = format;
            _data.Parameters = new[] { arg0, arg1, arg2, arg3 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public LogBuilder Message(string format, params object[] args)
        {
            _data.Message = format;
            _data.Parameters = args;

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public LogBuilder Message(IFormatProvider provider, string format, params object[] args)
        {
            _data.FormatProvider = provider;
            _data.Message = format;
            _data.Parameters = args;

            return this;
        }

        /// <summary>
        /// Sets a log context property on the logging event.
        /// </summary>
        /// <param name="name">The name of the context property.</param>
        /// <param name="value">The value of the context property.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public LogBuilder Property(string name, object value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (_data.Properties == null)
                _data.Properties = new Dictionary<string, object>();

            _data.Properties[name] = value;
            return this;
        }

        /// <summary>
        /// Sets the exception information of the logging event.
        /// </summary>
        /// <param name="exception">The exception information of the logging event.</param>
        /// <returns></returns>
        public LogBuilder Exception(Exception exception)
        {
            _data.Exception = exception;
            return this;
        }

        /// <summary>
        /// Writes the log event to the underlying logger.
        /// </summary>
        /// <param name="callerMemberName">The method or property name of the caller to the method. This is set at by the compiler.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller. This is set at by the compiler.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called. This is set at by the compiler.</param>
        public void Write(
            [CallerMemberName]string callerMemberName = null,
            [CallerFilePath]string callerFilePath = null,
            [CallerLineNumber]int callerLineNumber = 0)
        {
            if (callerMemberName != null)
                _data.MemberName = callerMemberName;
            if (callerFilePath != null)
                _data.FilePath = callerFilePath;
            if (callerLineNumber != 0)
                _data.LineNumber = callerLineNumber;

            _writer(_data);
        }


        /// <summary>
        /// Writes the log event to the underlying logger if the condition delegate is true.
        /// </summary>
        /// <param name="condition">If condition is true, write log event; otherwise ignore event.</param>
        /// <param name="callerMemberName">The method or property name of the caller to the method. This is set at by the compiler.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller. This is set at by the compiler.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called. This is set at by the compiler.</param>
        public void WriteIf(
            Func<bool> condition,
            [CallerMemberName]string callerMemberName = null,
            [CallerFilePath]string callerFilePath = null,
            [CallerLineNumber]int callerLineNumber = 0)
        {
            if (condition == null || !condition())
                return;

            Write(callerMemberName, callerFilePath, callerLineNumber);
        }

        /// <summary>
        /// Writes the log event to the underlying logger if the condition is true.
        /// </summary>
        /// <param name="condition">If condition is true, write log event; otherwise ignore event.</param>
        /// <param name="callerMemberName">The method or property name of the caller to the method. This is set at by the compiler.</param>
        /// <param name="callerFilePath">The full path of the source file that contains the caller. This is set at by the compiler.</param>
        /// <param name="callerLineNumber">The line number in the source file at which the method is called. This is set at by the compiler.</param>
        public void WriteIf(
            bool condition,
            [CallerMemberName]string callerMemberName = null,
            [CallerFilePath]string callerFilePath = null,
            [CallerLineNumber]int callerLineNumber = 0)
        {
            if (!condition)
                return;

            Write(callerMemberName, callerFilePath, callerLineNumber);
        }

    }

}
