using System;

namespace KickStart.Services
{
    /// <summary>
    /// Base class for <see cref="IServiceRegistration"/>
    /// </summary>
    public abstract class ServiceRegistrationBase : IServiceRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRegistrationBase"/> class.
        /// </summary>
        /// <param name="serviceContext">The current service <see cref="Context"/>.</param>
        protected ServiceRegistrationBase(Context serviceContext)
        {
            ServiceContext = serviceContext ?? throw new ArgumentNullException(nameof(serviceContext));
        }

        /// <summary>
        /// Gets the current service <see cref="Context"/>.
        /// </summary>
        /// <value>The current service <see cref="Context"/></value>
        public Context ServiceContext { get; }

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
        public abstract IServiceRegistration Register(Type serviceType, Type implementationType, ServiceLifetime lifetime);

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
        /// <seealso cref="ServiceLifetime.Singleton" />
        public abstract IServiceRegistration Register(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime);
    }
}