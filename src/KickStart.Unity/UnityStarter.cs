using KickStart.Logging;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    /// <summary>
    /// KickStart extension for Unity
    /// </summary>
    /// <seealso cref="KickStart.IKickStarter" />
    public class UnityStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<UnityStarter>();
        private readonly UnityOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityStarter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public UnityStarter(UnityOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="T:KickStart.Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var modules = context.GetInstancesAssignableFrom<IUnityRegistration>();

            var container = new UnityContainer();

            foreach (var module in modules)
            {
                _logger.Trace()
                    .Message("Register Unity Module: {0}", module)
                    .Write();

                module.Register(container);
            }

            _options.InitializeContainer?.Invoke(container);

            var provider = new UnityServiceProvider(container);
            context.SetServiceProvider(provider);
        }
    }
}