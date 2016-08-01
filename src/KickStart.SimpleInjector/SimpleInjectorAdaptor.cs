using System;
using System.Collections.Generic;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    public class SimpleInjectorAdaptor : IContainerAdaptor
    {
        private readonly Container _container;

        public SimpleInjectorAdaptor(Container container)
        {
            _container = container;
        }

        public TService Resolve<TService>()
            where TService : class
        {
            return _container.GetInstance<TService>();
        }

        public TService Resolve<TService>(string key)
            where TService : class
        {
            throw new NotSupportedException();
        }

        public object Resolve(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            return _container.GetInstance(serviceType);
        }

        public object Resolve(Type serviceType, string key)
        {

            throw new NotSupportedException();
        }

        public IEnumerable<TService> ResolveAll<TService>()
            where TService : class
        {
            return _container.GetAllInstances<TService>();
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            return _container.GetAllInstances(serviceType);
        }

        public TContainer As<TContainer>()
            where TContainer : class
        {
            return _container as TContainer;
        }
    }
}