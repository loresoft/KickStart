using System;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    public class UnityStarter : KickStarter
    {
        private readonly UnityOptions _options;

        public UnityStarter(UnityOptions options)
        {
            _options = options;
        }

        public override void Run(Context context)
        {
            var modules = GetInstancesAssignableFrom<IUnityRegistration>(context);

            var container = new UnityContainer();

            foreach (var module in modules)
            {
                Logger.Verbose()
                   .Message("Register Unity Module: {0}", module)
                   .Write();

                module.Register(container);
            }

            if (_options.InitializeContainer != null)
                _options.InitializeContainer(container);

            var adaptor = new UnityAdaptor(container);
            context.SetContainer(adaptor);
        }
    }
}