using System;
using Autofac;
using KickStart.Logging;

namespace KickStart.Autofac
{
    /// <summary>
    /// KickStart extension for Autofac
    /// </summary>
    public class AutofacStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<AutofacStarter>();
        private AutofacOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacStarter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AutofacStarter(AutofacOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var modules = context.GetInstancesAssignableFrom<Module>();

            var builder = new ContainerBuilder();

            foreach (var module in modules)
            {
                _logger.Trace()
                    .Message("Register Autofac Module: {0}", module)
                    .Write();

                builder.RegisterModule(module);
            }

            _options.InitializeBuilder?.Invoke(builder);

            _logger.Trace()
                .Message("Create Autofac Container...")
                .Write();

            var container = builder.Build(_options.BuildOptions);

            _options.InitializeContainer?.Invoke(container);

            var provider = new AutofacServiceProvider(container);
            context.SetServiceProvider(provider);
        }
    }
}