using System;
using KickStart.Services;
using Autofac;

namespace KickStart.Autofac
{    
    /// <summary>
    /// Autofac implementation for <see cref="IServiceRegistration"/>.
    /// </summary>
    /// <seealso cref="KickStart.Services.IServiceRegistration" />
    public class AutofacServiceRegistration : IServiceRegistration
    {
        private readonly ContainerBuilder _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacServiceRegistration"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AutofacServiceRegistration(ContainerBuilder container)
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

            var builder = _container
                .RegisterType(implementationType)
                .As(serviceType);


            if (lifetime == ServiceLifetime.Singleton)
                builder = builder.SingleInstance();


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
            var builder = _container
                .Register(c => implementationFactory(Wrap(c)))
                .As(serviceType);


            if (lifetime == ServiceLifetime.Singleton)
                builder = builder.SingleInstance();


            return this;
        }


        private static IServiceProvider Wrap(IComponentContext componentContext)
        {
            return new AutofacServiceProvider(componentContext);
        }
    }

}