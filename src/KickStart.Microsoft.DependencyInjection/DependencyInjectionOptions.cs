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
        /// Initializes a new instance of the <see cref="DependencyInjectionOptions"/> class.
        /// </summary>
        public DependencyInjectionOptions()
        {
            Creator = () => new ServiceCollection();
        }

        /// <summary>
        /// Gets or sets the service provider accessor <see langword="delegate" />.
        /// </summary>
        /// <value>
        /// The accessor.
        /// </value>
        public Action<IServiceProvider> Accessor { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IServiceCollection" /> creator <see langword="delegate" />.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection" /> creator <see langword="delegate" />.
        /// </value>
        public Func<IServiceCollection> Creator { get; set; }

        /// <summary>
        /// Gets or sets the initialize services <see langword="delegate" />.
        /// </summary>
        /// <value>
        /// The initialize services <see langword="delegate" />.
        /// </value>
        public Action<IServiceCollection> Initializer { get; set; }

    }

}
