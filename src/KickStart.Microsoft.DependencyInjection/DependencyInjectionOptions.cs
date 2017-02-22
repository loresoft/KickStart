using System;
using Microsoft.Extensions.DependencyInjection;


namespace KickStart.Microsoft.DependencyInjection
{
    /// <summary>
    /// Microsoft.Extensions.DependencyInjection options class
    /// </summary>
    public class DependencyInjectionOptions
    {
        /// <summary>
        /// Gets or sets the initialize services collection delegate.
        /// </summary>
        /// <value>
        /// The initialize services collection delegate.
        /// </value>
        public Action<IServiceCollection> Initializer { get; set; }

        /// <summary>
        /// Gets or sets the service collection.
        /// </summary>
        /// <value>
        /// The service collection.
        /// </value>
        public IServiceCollection ServiceCollection { get; set; } = new ServiceCollection();
    }

}
