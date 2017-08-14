using System;
using Serilog;

namespace KickStart
{
    /// <summary>
    /// Serilog Configuration interface
    /// </summary>
    public interface ISerilogConfiguration
    {
        /// <summary>
        /// Configure Serilog configuration options
        /// </summary>
        /// <param name="configuration">The Serilog configuration to update</param>
        void Configure(LoggerConfiguration configuration);
    }
}