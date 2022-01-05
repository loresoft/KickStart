using System;
using System.Collections.Generic;

namespace KickStart.Services
{
    public class ServiceRegistrationAttribute : Attribute
    {
        public Type ServiceType { get; set; }

        public Type ImplementationType { get; set; }
        
        public Func<IServiceProvider, object> ImplementationFactory { get; set; }

        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;
    }
}