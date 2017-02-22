using System;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    /// <summary>
    /// Unity adaptor for <see cref="IServiceProvider"/>
    /// </summary>
    public class UnityServiceProvider : IServiceProvider
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public UnityServiceProvider(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Gets the service object of the specified type
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>
        /// A service object of type serviceType.-or- null if there is no service object of type serviceType.
        /// </returns>
        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}