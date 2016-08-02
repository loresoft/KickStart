using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace KickStart.Ninject
{
    /// <summary>
    /// Ninject container adaptor
    /// </summary>
    public class NinjectAdaptor : IContainerAdaptor
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectAdaptor"/> class.
        /// </summary>
        /// <param name="kernel">The container.</param>
        /// <exception cref="System.ArgumentNullException">container</exception>
        public NinjectAdaptor(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException(nameof(kernel));

            _kernel = kernel;
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
                ? _kernel.Get<TService>()
                : _kernel.Get<TService>(key);
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
                ? _kernel.Get(serviceType)
                : _kernel.Get(serviceType, key);
        }

        /// <summary>
        /// Resolves all instances for the specified <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>A resolved instance of <typeparamref name="TService"/>.</returns>
        public IEnumerable<TService> ResolveAll<TService>() 
            where TService : class
        {
            return _kernel.GetAll<TService>();
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

            return _kernel.GetAll(serviceType);
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
            return _kernel as TContainer;
        }
    }
}