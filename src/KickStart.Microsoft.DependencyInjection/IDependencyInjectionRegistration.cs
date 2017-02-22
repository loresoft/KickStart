using Microsoft.Extensions.DependencyInjection;


namespace KickStart.Microsoft.DependencyInjection
{
    /// <summary>
    /// Microsoft.Extensions.DependencyInjection registration module
    /// </summary>
    public interface IDependencyInjectionRegistration
    {
        /// <summary>
        /// Register injections with the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The services collection.</param>
        void Register(IServiceCollection services);
    }

}
