using System;
using KickStart.SimpleInjector;
using SimpleInjector;

namespace Test.Core
{
    public class UserSimpleInjectorRegistration : ISimpleInjectorRegistration
    {
        public void Register(Container container)
        {
            container.Register<IUserRepository, UserRepository>();
        }
    }
}