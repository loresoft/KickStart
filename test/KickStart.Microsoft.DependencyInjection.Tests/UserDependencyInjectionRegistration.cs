using System;
using System.Collections.Generic;
using KickStart.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Test.Core;

namespace KickStart.Microsoft.DependencyInjection.Tests
{
    public class UserDependencyInjectionRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}