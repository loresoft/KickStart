using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace KickStart.Autofac
{
    /// <summary>
    /// Autofac container adaptor
    /// </summary>
    public class AutofacAdaptor : IContainerAdaptor
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacAdaptor"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <exception cref="System.ArgumentNullException">container</exception>
        public AutofacAdaptor(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        /// <summary>
        /// Resolves an instance for the specified <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>A resolved instance of <typeparamref name="TService"/>.</returns>
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
                : _container.ResolveNamed<TService>(key);
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
        public object Resolve(Type serviceType, string key)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            return key == null
                ? _container.Resolve(serviceType)
                : _container.ResolveNamed(key, serviceType);
        }

        /// <summary>
        /// Resolves all instances for the specified <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>A resolved instance of <typeparamref name="TService"/>.</returns>
        public IEnumerable<TService> ResolveAll<TService>() 
            where TService : class
        {
            return _container.Resolve<IEnumerable<TService>>();
        }

        /// <summary>
        /// Resolves  all instances for the specified <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        /// A resolved instance of <paramref name="serviceType" />.
        /// </returns>
        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);

            object instance = _container.Resolve(enumerableType);
            return ((IEnumerable)instance).Cast<object>();
        }

        /// <summary>
        /// Gets the underlying container cast to <typeparamref name="TContainer"/>. 
        /// </summary>
        /// <typeparam name="TContainer">The type of the container.</typeparam>
        /// <returns>
        /// An instance of <typeparamref name="TContainer"/>.
        /// </returns>
        public TContainer As<TContainer>() where TContainer : class
        {
            return _container as TContainer;
        }
    }
}