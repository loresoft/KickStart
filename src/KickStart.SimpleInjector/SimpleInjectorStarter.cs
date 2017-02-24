using System;
using KickStart.Services;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    /// <summary>
    /// SimpleInjector KickStarter extension
    /// </summary>
    /// <seealso cref="KickStart.IKickStarter" />
    public class SimpleInjectorStarter : IKickStarter
    {
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

            var container = new Container();

            RegisterSimpleInjector(context, container);
            RegisterServiceModule(context, container);

            _options.InitializeContainer?.Invoke(container);

            if (_options.VerificationOption.HasValue)
                container.Verify(_options.VerificationOption.Value);

            context.SetServiceProvider(container);
        }


        private void RegisterSimpleInjector(Context context, Container container)
        {
            var modules = context.GetInstancesAssignableFrom<ISimpleInjectorRegistration>();
            foreach (var module in modules)
            {
                context.WriteLog("Register SimpleInjector Module: {0}", module);

                module.Register(container, context.Data);
            }
        }

        private void RegisterServiceModule(Context context, Container container)
        {
            var wrapper = new SimpleInjectorRegistration(container);
            var modules = context.GetInstancesAssignableFrom<IServiceModule>();
            foreach (var module in modules)
            {
                context.WriteLog("Register Service Module: {0}", module);

                module.Register(wrapper, context.Data);
            }
        }
    }
}