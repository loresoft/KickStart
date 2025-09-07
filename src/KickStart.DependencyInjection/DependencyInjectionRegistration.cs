using KickStart.Services;

using Microsoft.Extensions.DependencyInjection;

using ServiceLifetime = KickStart.Services.ServiceLifetime;

namespace KickStart.DependencyInjection;

/// <summary>
/// Microsoft.Extensions.DependencyInjection implementation for <see cref="IServiceRegistration"/>.
/// </summary>
/// <seealso cref="IServiceRegistration" />
public class DependencyInjectionRegistration : ServiceRegistrationBase
{
    private readonly IServiceCollection _container;

    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyInjectionRegistration"/> class.
    /// </summary>
    /// <param name="container">The container.</param>
    /// <param name="serviceContext">The current service <see cref="Context"/>.</param>
    public DependencyInjectionRegistration(Context serviceContext, IServiceCollection container) : base(serviceContext)
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
            _container.AddSingleton(serviceType, implementationType);
        else if (lifetime == ServiceLifetime.Scoped)
            _container.AddScoped(serviceType, implementationType);
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
    public override IServiceRegistration Register(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
    {
        if (lifetime == ServiceLifetime.Singleton)
            _container.AddSingleton(serviceType, implementationFactory);
        else if (lifetime == ServiceLifetime.Scoped)
            _container.AddScoped(serviceType, implementationFactory);
        else
            _container.AddTransient(serviceType, implementationFactory);

        return this;
    }
}
