using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace KickStart
{

    /// <summary>
    /// A simple logging class
    /// </summary>
    public sealed class Logger
    {
        private static readonly TraceSwitch _traceSwitch = new TraceSwitch("KickStart", "KickStart Trace Switch");


        private TraceLevel _traceLevel;
        private string _message;
        private object[] _parameters;
        private IFormatProvider _formatProvider;
        private Exception _exception;
        private string _memberName;
        private string _filePath;
        private int _lineNumber;

        private Logger(TraceLevel traceLevel)
        {
            _traceLevel = traceLevel;
            _formatProvider = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Gets the global TraceSwitch used to filter the trace level output.
        /// </summary>
        /// <value>
        /// The trace switch.
        /// </value>
        public static TraceSwitch TraceSwitch
        {
            get { return _traceSwitch; }
        }


        /// <summary>
        /// Sets the level of the logging event.
        /// </summary>
        /// <param name="logLevel">The level of the logging event.</param>
        /// <returns></returns>
        public Logger Level(TraceLevel logLevel)
        {
            _traceLevel = logLevel;
            return this;
        }

        /// <summary>
        /// Sets the log message on the logging event.
        /// </summary>
        /// <param name="message">The log message for the logging event.</param>
        /// <returns></returns>
        public Logger Message(string message)
        {
            _message = message;

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format.</param>
        /// <returns></returns>
        public Logger Message(string format, object arg0)
        {
            _message = format;
            _parameters = new[] { arg0 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <returns></returns>
        public Logger Message(string format, object arg0, object arg1)
        {
            _message = format;
            _parameters = new[] { arg0, arg1 };

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
        public Logger Message(string format, object arg0, object arg1, object arg2)
        {
            _message = format;
            _parameters = new[] { arg0, arg1, arg2 };

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
        public Logger Message(string format, object arg0, object arg1, object arg2, object arg3)
        {
            _message = format;
            _parameters = new[] { arg0, arg1, arg2, arg3 };

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public Logger Message(string format, params object[] args)
        {
            _message = format;
            _parameters = args;

            return this;
        }

        /// <summary>
        /// Sets the log message and parameters for formating on the logging event.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public Logger Message(IFormatProvider provider, string format, params object[] args)
        {
            _formatProvider = provider;
            _message = format;
            _parameters = args;

            return this;
        }

        /// <summary>
        /// Sets the exception information of the logging event.
        /// </summary>
        /// <param name="exception">The exception information of the logging event.</param>
        /// <returns></returns>
        public Logger Exception(Exception exception)
        {
            _exception = exception;
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
                _memberName = callerMemberName;
            if (callerFilePath != null)
                _filePath = callerFilePath;
            if (callerLineNumber != 0)
                _lineNumber = callerLineNumber;

            bool isEnabled = Logger.TraceSwitch.Level >= _traceLevel;

            if (!isEnabled)
                return;

            var message = new StringBuilder();
            message
                .Append(DateTime.Now.ToString("HH:mm:ss.fff"))
                .Append(" [")
                .Append(_traceLevel.ToString().First())
                .Append("] ");

            if (!string.IsNullOrEmpty(_filePath) && !string.IsNullOrEmpty(_memberName))
            {
                message
                    .Append("[")
                    .Append(Path.GetFileName(_filePath))
                    .Append(" ")
                    .Append(_memberName)
                    .Append("()")
                    .Append(" Ln: ")
                    .Append(_lineNumber)
                    .Append("] ");
            }

            if (_parameters != null && _parameters.Length > 0)
                message.AppendFormat(_formatProvider, _message, _parameters);
            else
                message.Append(_message);

            if (_exception != null)
                message.Append(" ").Append(_exception);

            Trace.WriteLine(message);
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



        /// <summary>
        /// Start a fluent <see cref="TraceLevel.Verbose"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static Logger Verbose()
        {
            return new Logger(TraceLevel.Verbose);
        }

        /// <summary>
        /// Start a fluent <see cref="TraceLevel.Info"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static Logger Info()
        {
            return new Logger(TraceLevel.Info);
        }

        /// <summary>
        /// Start a fluent <see cref="TraceLevel.Warning"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static Logger Warning()
        {
            return new Logger(TraceLevel.Warning);
        }

        /// <summary>
        /// Start a fluent <see cref="TraceLevel.Error"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static Logger Error()
        {
            return new Logger(TraceLevel.Error);
        }
    }
}