using System;
using KickStart.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Test.Core;

namespace KickStart.Microsoft.DependencyInjection.Tests
{
    public class UserDependencyInjectionRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}