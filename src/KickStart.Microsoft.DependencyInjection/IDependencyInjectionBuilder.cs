using System;
using Microsoft.Extensions.DependencyInjection;


namespace KickStart.Microsoft.DependencyInjection
{
    /// <summary>
    /// Microsoft.Extensions.DependencyInjection configuration builder
    /// </summary>
    public interface IDependencyInjectionBuilder
    {
        /// <summary>
        /// Sets the initial service collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        IDependencyInjectionBuilder Collection(IServiceCollection collection);

        /// <summary>
        /// Sets the initialize container <see langword="delegate"/>.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <returns></returns>
        IDependencyInjectionBuilder Services(Action<IServiceCollection> initializer);
    }

}
