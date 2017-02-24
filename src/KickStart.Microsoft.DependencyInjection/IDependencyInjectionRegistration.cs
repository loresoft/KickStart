using System.Collections.Generic;
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
        /// <param name="data">The data dictionary shared with all starter modules.</param>
        void Register(IServiceCollection services, IDictionary<string, object> data);
    }

}
