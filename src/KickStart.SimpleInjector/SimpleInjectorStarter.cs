using System;
using KickStart.Logging;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    public class SimpleInjectorStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<SimpleInjectorStarter>();
        private readonly SimpleInjectorOptions _options;

        public SimpleInjectorStarter(SimpleInjectorOptions options)
        {
            _options = options;
        }

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

            if (_options.InitializeContainer != null)
                _options.InitializeContainer(container);

            var adaptor = new SimpleInjectorAdaptor(container);
            context.SetContainer(adaptor);
        }
    }
}