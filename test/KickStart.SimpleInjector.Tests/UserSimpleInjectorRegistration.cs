using System;
using KickStart.SimpleInjector;
using SimpleInjector;
using Test.Core;

namespace KickStart.SimpleInjector.Tests
{
    public class UserSimpleInjectorRegistration : ISimpleInjectorRegistration
    {
        public void Register(Container container)
        {
            container.Register<IUserRepository, UserRepository>();
        }
    }
}