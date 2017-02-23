namespace KickStart.Services
{
    /// <summary>
    /// Service registration module
    /// </summary>
    public interface IServiceModule
    {
        /// <summary>
        /// Register service injections with the specified <paramref name="services"/> container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceRegistration"/> to add the module services to.</param>
        void Register(IServiceRegistration services);
    }

}