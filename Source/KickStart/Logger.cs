using System;
using System.Diagnostics;
using System.Threading;

namespace KickStart
{
    /// <summary>
    /// A simple logging class
    /// </summary>
    public sealed class Logger
    {
        private static readonly Lazy<TraceSwitch> _traceSwitch;
        
        static Logger()
        {
            _traceSwitch = new Lazy<TraceSwitch>(() => new TraceSwitch("KickStart", "KickStart Trace Switch"));
        }

        /// <summary>
        /// Gets the global TraceSwitch used to filter the trace level output.
        /// </summary>
        /// <value>
        /// The trace switch.
        /// </value>
        public static TraceSwitch TraceSwitch
        {
            get { return _traceSwitch.Value; }
        }

        /// <summary>
        /// The <see langword="delegate"/> to write log messages to.
        /// </summary>
        public static Action<LogData> LogWritter { get; set; }

        /// <summary>
        /// Start a fluent <see cref="TraceLevel.Verbose"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Verbose()
        {
            return new LogBuilder(TraceLevel.Verbose, LogWritter ?? TraceWrite);
        }

        /// <summary>
        /// Start a fluent <see cref="TraceLevel.Info"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Info()
        {
            return new LogBuilder(TraceLevel.Info, LogWritter ?? TraceWrite);
        }

        /// <summary>
        /// Start a fluent <see cref="TraceLevel.Warning"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Warning()
        {
            return new LogBuilder(TraceLevel.Warning, LogWritter ?? TraceWrite);
        }

        /// <summary>
        /// Start a fluent <see cref="TraceLevel.Error"/> logger.
        /// </summary>
        /// <returns>A fluent Logger instance.</returns>
        public static LogBuilder Error()
        {
            return new LogBuilder(TraceLevel.Error, LogWritter ?? TraceWrite);
        }


        /// <summary>
        /// Writes log messages to <see cref="Trace"/>.
        /// </summary>
        /// <param name="data">The LogData to write.</param>
        public static void TraceWrite(LogData data)
        {
            bool isEnabled = TraceSwitch.Level >= data.TraceLevel;

            if (!isEnabled)
                return;

            Trace.WriteLine(data);
        }

    }
}