using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace KickStart.Autofac
{
    public class AutofacAdaptor : IContainerAdaptor
    {
        private readonly IContainer _container;

        public AutofacAdaptor(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        public TService Resolve<TService>()
        {
            return Resolve<TService>(null);
        }

        public TService Resolve<TService>(string key)
        {
            return key == null
                ? _container.Resolve<TService>()
                : _container.ResolveNamed<TService>(key);
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
                : _container.ResolveNamed(key, serviceType);
        }

        public IEnumerable<TService> ResolveAll<TService>()
        {
            return _container.Resolve<IEnumerable<TService>>();
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);

            object instance = _container.Resolve(enumerableType);
            return ((IEnumerable)instance).Cast<object>();
        }
    }
}