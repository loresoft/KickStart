using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{

    /// <summary>
    /// Unity container adaptor
    /// </summary>
    /// <seealso cref="KickStart.IContainerAdaptor" />
    public class UnityAdaptor : IContainerAdaptor
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityAdaptor"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UnityAdaptor(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Resolves an instance for the specified <typeparamref name="TService" /> type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>
        /// A resolved instance of <typeparamref name="TService" />.
        /// </returns>
        public TService Resolve<TService>()
            where TService : class
        {
            return Resolve<TService>(null);
        }

        /// <summary>
        /// Resolves an instance for the specified <typeparamref name="TService" /> type and <paramref name="key" />.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="key">The name of a keyed instance.</param>
        /// <returns>
        /// A resolved instance of <typeparamref name="TService" />.
        /// </returns>
        public TService Resolve<TService>(string key)
            where TService : class
        {
            return key == null
                ? _container.Resolve<TService>()
                : _container.Resolve<TService>(key);
        }

        /// <summary>
        /// Resolves an instance for the specified <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        /// A resolved instance of <paramref name="serviceType" />.
        /// </returns>
        public object Resolve(Type serviceType)
        {
            return Resolve(serviceType, null);
        }

        /// <summary>
        /// Resolves an instance for the specified <paramref name="serviceType" /> and <paramref name="key" />.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The name of a keyed instance.</param>
        /// <returns>
        /// A resolved instance of <paramref name="serviceType" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public object Resolve(Type serviceType, string key)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            return key == null
                ? _container.Resolve(serviceType)
                : _container.Resolve(serviceType, key);
        }

        /// <summary>
        /// Resolves all instances for the specified <typeparamref name="TService" /> type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>
        /// A resolved instance of <typeparamref name="TService" />.
        /// </returns>
        public IEnumerable<TService> ResolveAll<TService>()
            where TService : class
        {
            return _container.ResolveAll<TService>();
        }

        /// <summary>
        /// Resolves  all instances for the specified <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        /// A resolved instance of <paramref name="serviceType" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            return _container.ResolveAll(serviceType);
        }

        /// <summary>
        /// Gets the underlying container cast to <typeparamref name="TContainer" />.
        /// </summary>
        /// <typeparam name="TContainer">The type of the container.</typeparam>
        /// <returns>
        /// An instance of <typeparamref name="TContainer" />.
        /// </returns>
        public TContainer As<TContainer>()
            where TContainer : class
        {
            return _container as TContainer;
        }
    }
}