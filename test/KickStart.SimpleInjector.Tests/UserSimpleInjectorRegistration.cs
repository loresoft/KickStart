using System;
using System.Collections.Generic;
using SimpleInjector;
using Test.Core;

namespace KickStart.SimpleInjector.Tests
{
    public class UserSimpleInjectorRegistration : ISimpleInjectorRegistration
    {
        public void Register(Container container, IDictionary<string, object> data)
        {
            container.Register<IUserRepository, UserRepository>();
            container.Register<InjectorStartTask>();
        }
    }
}