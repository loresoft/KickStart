namespace KickStart.Services
{
    /// <summary>
    /// Specifies the lifetime of a service in an <see cref="IServiceRegistration"/>.
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// Specifies that a single instance of the service will be created.
        /// </summary>
        Singleton,
        /// <summary>
        /// Specifies that a new instance of the service will be created for each scope.
        /// </summary>
        Scoped,
        /// <summary>
        /// Specifies that a new instance of the service will be created every time it is requested.
        /// </summary>
        Transient
    }
}