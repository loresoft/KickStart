using System;
using Microsoft.Extensions.DependencyInjection;


namespace KickStart.Microsoft.DependencyInjection
{
    /// <summary>
    /// Microsoft.Extensions.DependencyInjection configuration builder
    /// </summary>
    public class DependencyInjectionBuilder : IDependencyInjectionBuilder
    {
        private readonly DependencyInjectionOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionBuilder"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DependencyInjectionBuilder(DependencyInjectionOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Sets the initial service collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public IDependencyInjectionBuilder Collection(IServiceCollection collection)
        {
            _options.ServiceCollection = collection;
            return this;
        }

        /// <summary>
        /// Sets the initialize container <see langword="delegate" />.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <returns></returns>
        public IDependencyInjectionBuilder Services(Action<IServiceCollection> initializer)
        {
            _options.Initializer = initializer;
            return this;
        }
    }

}
