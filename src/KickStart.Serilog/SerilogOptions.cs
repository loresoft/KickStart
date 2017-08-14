using System;
using Serilog;

namespace KickStart
{
    /// <summary>
    /// Options for Serilog
    /// </summary>
    public class SerilogOptions
    {
        /// <summary>
        /// Gets or sets the <see langword="delegate"/> to call for additional configuration.
        /// </summary>
        /// <value>
        /// The <see langword="delegate"/> to call for additional configuration.
        /// </value>
        public Action<LoggerConfiguration> Configure { get; set; }
    }
}