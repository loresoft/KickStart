using System;

// ReSharper disable once CheckNamespace
namespace KickStart
{
    /// <summary>
    /// KickStart Extension for Serilog.
    /// </summary>
    public static class SerilogExtensions
    {
        /// <summary>
        /// Use Serilog as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseSerilog(this IConfigurationBuilder configurationBuilder)
        {
            return UseSerilog(configurationBuilder, null);
        }


        /// <summary>
        /// Use Serilog as a logging target.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="configure">The configure action for Serilog.</param>
        /// <returns></returns>
        public static IConfigurationBuilder UseSerilog(this IConfigurationBuilder configurationBuilder, Action<Serilog.LoggerConfiguration> configure)
        {
            var options = new SerilogOptions { Configure = configure };
            var starter = new SerilogStarter(options);

            configurationBuilder.ExcludeAssemblyFor<SerilogStarter>();
            configurationBuilder.ExcludeAssemblyFor<Serilog.ILogger>();
            configurationBuilder.ExcludeName("Serilog");

            configurationBuilder.Use(starter);

            // register log writer
            configurationBuilder.LogTo(Serilog.Log.Debug);

            return configurationBuilder;
        }

    }
}
