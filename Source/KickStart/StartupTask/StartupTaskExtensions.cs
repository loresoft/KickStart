using System;

namespace KickStart.StartupTask
{
    /// <summary>
    /// Extension methods to use configure startup tasks
    /// </summary>
    public static class StartupTaskExtensions
    {
        /// <summary>
        /// Use the startup task extension to execute all instances of <see cref="IStartupTask"/>.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <returns>
        /// A fluent <see langword="interface"/> to configure startup tasks
        /// </returns>
        /// <example>Configure KickStart to use startup tasks
        /// <code><![CDATA[
        /// Kick.Start(config => config
        ///     .IncludeAssemblyFor<TestStartup>()
        ///     .UseStartupTask()
        ///     .LogLevel(TraceLevel.Verbose)
        /// );]]></code>
        /// </example>
        public static IConfigurationBuilder UseStartupTask(this IConfigurationBuilder configurationBuilder)
        {
            return UseStartupTask(configurationBuilder, null);
        }

        /// <summary>
        /// Use the startup task extension to execute all instances of <see cref="IStartupTask" />.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="configure">The <see langword="delegate"/> to configure StartupTask options.</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to configure startup tasks
        /// </returns>
        /// <example>Configure KickStart to use startup tasks using Autofac container to resolve <see cref="IStartupTask" /> instances.
        /// <code><![CDATA[
        /// Kick.Start(config => config
        ///     .IncludeAssemblyFor<TestStartup>()
        ///     .UseAutofac()
        ///     .UseStartupTask(c => c.UseContainer())
        ///     .LogLevel(TraceLevel.Verbose)
        /// );]]></code>
        /// </example>
        public static IConfigurationBuilder UseStartupTask(this IConfigurationBuilder configurationBuilder, Action<IStartupTaskBuilder> configure)
        {
            var options = new StartupTaskOptions();
            var starter = new StartupTaskStarter(options);

            if (configure != null)
            {
                var builder = new StartupTaskBuilder(options);
                configure(builder);
            }

            configurationBuilder.Use(starter);

            return configurationBuilder;
        }
    }
}