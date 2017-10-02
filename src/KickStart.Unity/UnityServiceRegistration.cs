using System;
using KickStart.Services;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    /// <summary>
    /// Unity implementation for <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <seealso cref="IServiceRegistration" />
    public class UnityServiceRegistration : ServiceRegistrationBase
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityServiceRegistration"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="serviceContext">The current service <see cref="Context"/>.</param>
        public UnityServiceRegistration(Context serviceContext, IUnityContainer container) : base(serviceContext)
        {
            _container = container;
        }

        /// <summary>
        /// Registers a service of the type specified in <paramref name="serviceType" /> with an
        /// implementation of the type specified in <paramref name="implementationType" /> using
        /// the specified <paramref name="lifetime" />.
        /// </summary>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <param name="lifetime">The service lifetime.</param>
        /// <returns>
        /// A reference to this instance after the operation has completed.
        /// </returns>
        public override IServiceRegistration Register(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            var lifetimeManager = lifetime == ServiceLifetime.Singleton ? new ContainerControlledLifetimeManager() : null;
            var builder = _container.RegisterType(serviceType, implementationType, lifetimeManager);

            return this;
        }

        /// <summary>
        /// Registers a service of the type specified in <paramref name="serviceType" /> with a
        /// factory specified in <paramref name="implementationFactory" /> using
        /// the specified <paramref name="lifetime" />.
        /// </summary>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="lifetime">The service lifetime.</param>
        /// <returns>
        /// A reference to this instance after the operation has completed.
        /// </returns>
        /// <seealso cref="F:KickStart.Services.ServiceLifetime.Singleton" />
        public override IServiceRegistration Register(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            throw new NotSupportedException();
        }
    }

}