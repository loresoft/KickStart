using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    public class UnityAdaptor : IContainerAdaptor
    {
        private readonly IUnityContainer _container;

        public UnityAdaptor(IUnityContainer container)
        {
            _container = container;
        }

        public TService Resolve<TService>()
            where TService : class
        {
            return Resolve<TService>(null);
        }

        public TService Resolve<TService>(string key)
            where TService : class
        {
            return key == null
                ? _container.Resolve<TService>()
                : _container.Resolve<TService>(key);
        }

        public object Resolve(Type serviceType)
        {
            return Resolve(serviceType, null);
        }

        public object Resolve(Type serviceType, string key)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            return key == null
                ? _container.Resolve(serviceType)
                : _container.Resolve(serviceType, key);
        }

        public IEnumerable<TService> ResolveAll<TService>()
            where TService : class
        {
            return _container.ResolveAll<TService>();
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            return _container.ResolveAll(serviceType);
        }

        public TContainer As<TContainer>()
            where TContainer : class
        {
            return _container as TContainer;
        }
    }
}