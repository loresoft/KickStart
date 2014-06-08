using System;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    public class SimpleInjectorStarter : KickStarter
    {
        private readonly SimpleInjectorOptions _options;

        public SimpleInjectorStarter(SimpleInjectorOptions options)
        {
            _options = options;
        }

        public override void Run(Context context)
        {
            var modules = GetInstancesAssignableFrom<ISimpleInjectorRegistration>(context);

            var container = new Container();

            foreach (var module in modules)
            {
                Logger.Verbose()
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