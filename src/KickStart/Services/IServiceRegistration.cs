namespace KickStart.Services;

/// <summary>
/// An <see langword="interface"/> for service registration
/// </summary>
public interface IServiceRegistration
{
    /// <summary>
    /// Gets the current service <see cref="Context"/>.
    /// </summary>
    /// <value>The current service <see cref="Context"/></value>
    Context ServiceContext { get; }

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
    IServiceRegistration Register(Type serviceType, Type implementationType, ServiceLifetime lifetime);

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
    IServiceRegistration Register(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime);

}