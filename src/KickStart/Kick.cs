using System;
using System.Collections.Generic;
using System.Linq;
#if PORTABLE
using Stopwatch = KickStart.Portability.Stopwatch;
#else
using Stopwatch = System.Diagnostics.Stopwatch;
#endif

namespace KickStart
{
    /// <summary>
    /// Initialize an application at startup using the KickStart extensions.
    /// </summary>
    public static class Kick
    {
        /// <summary>
        /// Gets a common IoC container that can be used in KickStart extensions.
        /// </summary>
        /// <value>
        /// The common IoC container.
        /// </value>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Gets the configuration data used for startup.
        /// </summary>
        /// <value>
        /// The configuration data used for startup.
        /// </value>
        public static IDictionary<string, object> Data { get; private set; }

        /// <summary>
        /// Configure and run the KickStart extensions.
        /// </summary>
        /// <param name="configurator">The <see langword="delegate"/> to configure KickStart before execution of the extensions.</param>
        /// <example>Configure KickStart to use startup tasks using Autofac container to resolve <see cref="T:KickStart.StartupTask.IStartupTask" /> instances.
        /// <code><![CDATA[
        /// Kick.Start(config => config
        ///     .IncludeAssemblyFor<TestStartup>()
        ///     .UseAutofac()
        ///     .UseStartupTask(c => c.UseContainer())
        ///     .LogLevel(TraceLevel.Verbose)
        /// );]]></code>
        /// </example>
        public static void Start(Action<IConfigurationBuilder> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var config = new Configuration();
            var builder = new ConfigurationBuilder(config);

            configurator(builder);
            
            var assemblies = config.Assemblies.Resolve();

            // cache types
            var types = assemblies
                .SelectMany(a => a.GetLoadableTypes())
                .ToList();

            var context = new Context(types, config.Data, config.LogWriter);

            foreach (var starter in config.Starters)
            {
                context.WriteLog("Execute Starter: {0}", starter);

                var watch = Stopwatch.StartNew();

                starter.Run(context);

                watch.Stop();

                context.WriteLog("Completed Starter: {0}, Time: {1} ms", starter, watch.ElapsedMilliseconds);
            }

            // save service provider
            ServiceProvider = context.ServiceProvider;
            Data = context.Data;
        }
    }
}
