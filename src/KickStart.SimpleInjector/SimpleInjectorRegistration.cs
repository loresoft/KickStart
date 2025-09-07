using KickStart.Services;

using SimpleInjector;

namespace KickStart.SimpleInjector;

/// <summary>
/// SimpleInjector implementation for <see cref="IServiceRegistration"/>.
/// </summary>
/// <seealso cref="IServiceRegistration" />
public class SimpleInjectorRegistration : ServiceRegistrationBase
{
    private readonly Container _container;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleInjectorRegistration"/> class.
    /// </summary>
    /// <param name="container">The container.</param>
    /// <param name="serviceContext">The current service <see cref="Context"/>.</param>
    public SimpleInjectorRegistration(Context serviceContext, Container container) : base(serviceContext)
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
        if (lifetime == ServiceLifetime.Singleton)
            _container.RegisterSingleton(serviceType, implementationType);
        else
            _container.Register(serviceType, implementationType);

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
        if (lifetime == ServiceLifetime.Singleton)
            _container.RegisterSingleton(serviceType, () => implementationFactory(_container));
        else
            _container.Register(serviceType, () => implementationFactory(_container));

        return this;
    }
}