using System;
using KickStart.Logging;
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
        private static readonly ILogger _logger = Logger.CreateLogger(typeof(Kick));
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// Gets a common IoC container that can be used in KickStart extensions.
        /// </summary>
        /// <value>
        /// The common IoC container.
        /// </value>
        public static IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

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
            var context = new Context(assemblies);

            foreach (var starter in config.Starters)
            {
                _logger.Trace()
                    .Message("Execute Starter: {0}", starter)
                    .Write();

                var watch = Stopwatch.StartNew();

                starter.Run(context);

                watch.Stop();

                _logger.Trace()
                    .Message("Completed Starter: {0}, Time: {1} ms", starter, watch.ElapsedMilliseconds)
                    .Write();
            }
        }

        /// <summary>
        /// Sets the global <see cref="P:KickStart.Kick.ServiceProvider"/>.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to set.</param>
        internal static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
