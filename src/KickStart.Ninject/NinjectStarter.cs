using System;
using System.Linq;
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
                context.WriteLog("Register Ninject Module: {0}", module);
            }

            context.WriteLog("Create Ninject Kernel...");

            var settings = _options.Settings ?? new NinjectSettings();
            var kernel = new StandardKernel(settings, modules);

            _options.InitializeKernel?.Invoke(kernel);

            context.SetServiceProvider(kernel);
        }
    }
}