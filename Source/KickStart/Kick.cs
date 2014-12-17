using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace KickStart
{
    /// <summary>
    /// Initialize an application at startup using the KickStart extensions.
    /// </summary>
    public static class Kick
    {
        private static IContainerAdaptor _container;

        /// <summary>
        /// Gets a common IoC container that can be used in KickStart extensions.
        /// </summary>
        /// <value>
        /// The common IoC container.
        /// </value>
        public static IContainerAdaptor Container
        {
            get { return _container; }
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
            var config = new Configuration();
            var builder = new ConfigurationBuilder(config);

            configurator(builder);

            var assemblies = config.Assemblies.Resolve();
            var context = new Context(assemblies);

            foreach (var starter in config.Starters)
            {
                Logger.Trace()
                    .Logger(typeof(Kick).FullName)
                    .Message("Execute Starter: {0}", starter)
                    .Write();

                Stopwatch watch = Stopwatch.StartNew();

                starter.Run(context);

                watch.Stop();

                Logger.Trace()
                    .Logger(typeof(Kick).FullName)
                    .Message("Completed Starter: {0}, Time: {1} ms", starter, watch.ElapsedMilliseconds)
                    .Write();
            }
        }

        /// <summary>
        /// Sets the global <see cref="P:KickStart.Kick.Container"/>.
        /// </summary>
        /// <param name="container">The container adaptor to assign.</param>
        internal static void SetContainer(IContainerAdaptor container)
        {
            _container = container;
        }
    }
}
