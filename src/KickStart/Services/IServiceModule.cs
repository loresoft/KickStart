using System.Collections.Generic;

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
        /// <param name="services">The <see cref="IServiceRegistration"/> container to add the module services to.</param>
        /// <param name="data">The data dictionary shared with all starter modules.</param>
        void Register(IServiceRegistration services, IDictionary<string, object> data);
    }

}