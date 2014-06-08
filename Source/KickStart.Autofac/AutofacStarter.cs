using System;
using Autofac;
using Autofac.Core;

namespace KickStart.Autofac
{
    public class AutofacStarter : KickStarter
    {
        private AutofacOptions _options;
        public AutofacStarter(AutofacOptions options)
        {
            _options = options;
        }

        public override void Run(Context context)
        {
            var modules = GetInstancesAssignableFrom<Module>(context);

            var builder = new ContainerBuilder();

            foreach (var module in modules)
            {
                Logger.Verbose()
                   .Message("Register Autofac Module: {0}", module)
                   .Write();

                builder.RegisterModule(module);
            }

            if (_options.InitializeBuilder != null)
                _options.InitializeBuilder(builder);

            Logger.Verbose()
               .Message("Create Autofac Container...")
               .Write();

            var container = builder.Build(_options.BuildOptions);

            if (_options.InitializeContainer != null)
                _options.InitializeContainer(container);
            
            var adaptor = new AutofacAdaptor(container);
            context.SetContainer(adaptor);
        }
    }
}