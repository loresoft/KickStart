using System;
using KickStart.Services;
using Microsoft.Extensions.DependencyInjection;
using ServiceLifetime = KickStart.Services.ServiceLifetime;

namespace KickStart.Microsoft.DependencyInjection
{
    /// <summary>
    /// Microsoft.Extensions.DependencyInjection implementation for <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <seealso cref="KickStart.Services.IServiceRegistration" />
    public class DependencyInjectionRegistration : IServiceRegistration
    {
        private readonly IServiceCollection _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionRegistration"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public DependencyInjectionRegistration(IServiceCollection container)
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
        public IServiceRegistration Register(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            if (lifetime == ServiceLifetime.Singleton)
                _container.AddSingleton(serviceType, implementationType);
            else
                _container.AddTransient(serviceType, implementationType);

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
        public IServiceRegistration Register(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            if (lifetime == ServiceLifetime.Singleton)
                _container.AddSingleton(serviceType, implementationFactory);
            else
                _container.AddTransient(serviceType, implementationFactory);

            return this;
        }
    }


}
