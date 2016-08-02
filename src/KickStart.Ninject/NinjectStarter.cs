using System;
using System.Linq;
using KickStart.Logging;
using Ninject;
using Ninject.Modules;

namespace KickStart.Ninject
{
    /// <summary>
    /// Ninject KickStart extention
    /// </summary>
    /// <seealso cref="KickStart.IKickStarter" />
    public class NinjectStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<NinjectStarter>();
        private readonly NinjectOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectStarter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public NinjectStarter(NinjectOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="T:KickStart.Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var modules = context.GetInstancesAssignableFrom<INinjectModule>().ToArray();

            foreach (var module in modules)
            {
                _logger.Trace()
                    .Message("Register Ninject Module: {0}", module)
                    .Write();
            }

            _logger.Trace()
                .Message("Create Ninject Kernel...")
                .Write();

            var settings = _options.Settings ?? new NinjectSettings();
            var kernel = new StandardKernel(settings, modules);

            _options.InitializeKernel?.Invoke(kernel);
            
            var adaptor = new NinjectAdaptor(kernel);
            context.SetContainer(adaptor);
        }
    }
}