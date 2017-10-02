using KickStart.Services;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    /// <summary>
    /// KickStart extension for Unity
    /// </summary>
    /// <seealso cref="KickStart.IKickStarter" />
    public class UnityStarter : IKickStarter
    {
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

            var container = new UnityContainer();

            RegisterUnity(context, container);
            RegisterServiceModule(context, container);

            _options.InitializeContainer?.Invoke(container);

            var provider = new UnityServiceProvider(container);
            context.SetServiceProvider(provider);
        }

        private void RegisterUnity(Context context, IUnityContainer container)
        {
            var modules = context.GetInstancesAssignableFrom<IUnityRegistration>();
            foreach (var module in modules)
            {
                context.WriteLog("Register Unity Module: {0}", module);

                module.Register(container);
            }
        }


        private void RegisterServiceModule(Context context, IUnityContainer container)
        {
            var wrapper = new UnityServiceRegistration(context, container);
            var modules = context.GetInstancesAssignableFrom<IServiceModule>();
            foreach (var module in modules)
            {
                context.WriteLog("Register Service Module: {0}", module);

                module.Register(wrapper, context.Data);
            }
        }

    }
}