using System;
using System.Reflection;
using KickStart.Services;
using Microsoft.Extensions.DependencyInjection;


namespace KickStart.DependencyInjection
{
    /// <summary>
    /// Microsoft.Extensions.DependencyInjection KickStarter extension
    /// </summary>
    /// <seealso cref="KickStart.IKickStarter" />
    public class DependencyInjectionStarter : IKickStarter
    {
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
            var serviceCollection = _options?.Creator() ?? new ServiceCollection();

            _options?.Initializer?.Invoke(serviceCollection);

            RegisterDependencyInjection(context, serviceCollection);
            RegisterServiceModule(context, serviceCollection);
            RegisterServiceAttributes(context, serviceCollection);

            var provider = serviceCollection.BuildServiceProvider();

            _options?.Accessor?.Invoke(provider);

            context.SetServiceProvider(provider);
        }

        private void RegisterServiceAttributes(Context context, IServiceCollection serviceCollection)
        {
            var types = context.GetTypesWithAttribute<ServiceRegistrationAttribute>();
            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute(typeof(ServiceRegistrationAttribute));

                

            }

        }

        private void RegisterDependencyInjection(Context context, IServiceCollection serviceCollection)
        {
            var modules = context.GetInstancesAssignableFrom<IDependencyInjectionRegistration>();
            foreach (var module in modules)
            {
                context.WriteLog("Register DependencyInjection Module: {0}", module);

                module.Register(serviceCollection, context.Data);
            }
        }

        private void RegisterServiceModule(Context context, IServiceCollection serviceCollection)
        {
            var wrapper = new DependencyInjectionRegistration(context, serviceCollection);
            var modules = context.GetInstancesAssignableFrom<IServiceModule>();
            foreach (var module in modules)
            {
                context.WriteLog("Register Service Module: {0}", module);

                module.Register(wrapper, context.Data);
            }

            wrapper.RegisterSingleton(builder =>
            {
                builder
                    .Types(t => t.Where(v => v.GetCustomAttribute<ServiceRegistrationAttribute>() != null))
                    .As(s => s.Self().ImplementedInterfaces())
                    .With(context.);
            });
        }

    }

}
