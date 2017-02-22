using System;
using KickStart.Logging;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    /// <summary>
    /// SimpleInjector KickStarter extension
    /// </summary>
    /// <seealso cref="KickStart.IKickStarter" />
    public class SimpleInjectorStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<SimpleInjectorStarter>();
        private readonly SimpleInjectorOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorStarter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SimpleInjectorStarter(SimpleInjectorOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="T:KickStart.Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var modules = context.GetInstancesAssignableFrom<ISimpleInjectorRegistration>();

            var container = new Container();

            foreach (var module in modules)
            {
                _logger.Trace()
                    .Message("Register SimpleInjector Module: {0}", module)
                    .Write();

                module.Register(container);
            }

            _options.InitializeContainer?.Invoke(container);

            context.SetServiceProvider(container);
        }
    }
}