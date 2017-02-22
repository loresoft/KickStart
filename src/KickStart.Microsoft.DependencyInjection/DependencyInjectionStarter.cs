using System;
using KickStart.Logging;
using Microsoft.Extensions.DependencyInjection;


namespace KickStart.Microsoft.DependencyInjection
{
    /// <summary>
    /// Microsoft.Extensions.DependencyInjection KickStarter extension
    /// </summary>
    /// <seealso cref="KickStart.IKickStarter" />
    public class DependencyInjectionStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<DependencyInjectionStarter>();
        private readonly DependencyInjectionOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionStarter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DependencyInjectionStarter(DependencyInjectionOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="T:KickStart.Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var modules = context.GetInstancesAssignableFrom<IDependencyInjectionRegistration>();
            var serviceCollection = _options.ServiceCollection ?? new ServiceCollection();

            foreach (var module in modules)
            {
                _logger.Trace()
                    .Message("Register DependencyInjection Module: {0}", module)
                    .Write();

                module.Register(serviceCollection);
            }

            _options.Initializer?.Invoke(serviceCollection);

            var provider = serviceCollection.BuildServiceProvider();
            context.SetServiceProvider(provider);
        }
    }

}
