using System;
using System.Collections.Generic;

namespace KickStart
{
    /// <summary>
    /// An <see langword="interface"/> for a simple warpper implementation of an inversion of control container.
    /// </summary>
    public interface IContainerAdaptor
    {
        /// <summary>
        /// Resolves an instance for the specified <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>A resolved instance of <typeparamref name="TService"/>.</returns>
        TService Resolve<TService>();

        /// <summary>
        /// Resolves an instance for the specified <typeparamref name="TService" /> type and <paramref name="key" />.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="key">The name of a keyed instance.</param>
        /// <returns>
        /// A resolved instance of <typeparamref name="TService" />.
        /// </returns>
        TService Resolve<TService>(string key);


        /// <summary>
        /// Resolves an instance for the specified <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        /// A resolved instance of <paramref name="serviceType" />.
        /// </returns>
        object Resolve(Type serviceType);

        /// <summary>
        /// Resolves an instance for the specified <paramref name="serviceType" /> and <paramref name="key" />.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The name of a keyed instance.</param>
        /// <returns>
        /// A resolved instance of <paramref name="serviceType" />.
        /// </returns>
        object Resolve(Type serviceType, string key);


        /// <summary>
        /// Resolves all instances for the specified <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>A resolved instance of <typeparamref name="TService"/>.</returns>
        IEnumerable<TService> ResolveAll<TService>();

        /// <summary>
        /// Resolves  all instances for the specified <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        /// A resolved instance of <paramref name="serviceType" />.
        /// </returns>
        IEnumerable<object> ResolveAll(Type serviceType);
    }
}