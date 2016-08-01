using System;
using System.Linq;
using KickStart.Logging;
using Ninject;
using Ninject.Modules;

namespace KickStart.Ninject
{
    public class NinjectStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<NinjectStarter>();
        private readonly NinjectOptions _options;

        public NinjectStarter(NinjectOptions options)
        {
            _options = options;
        }

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

            if (_options.InitializeKernel != null)
                _options.InitializeKernel(kernel);


            var adaptor = new NinjectAdaptor(kernel);
            context.SetContainer(adaptor);
        }
    }
}