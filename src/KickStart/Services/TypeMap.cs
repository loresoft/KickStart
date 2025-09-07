namespace KickStart.Services;

/// <summary>
/// Implementation to service type mapping
/// </summary>
public struct TypeMap
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypeMap"/> class.
    /// </summary>
    /// <param name="implementationType">The implementation type</param>
    /// <param name="serviceTypes">The service types</param>
    public TypeMap(Type implementationType, IEnumerable<Type> serviceTypes)
    {
        ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
        ServiceTypes = serviceTypes ?? throw new ArgumentNullException(nameof(serviceTypes));
    }

    /// <summary>
    /// Gets the implementation type
    /// </summary>
    public Type ImplementationType { get; }

    /// <summary>
    ///Gets the service types
    /// </summary>
    public IEnumerable<Type> ServiceTypes { get; }
}