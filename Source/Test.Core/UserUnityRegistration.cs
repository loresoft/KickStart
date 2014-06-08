using System;
using KickStart.Unity;
using Microsoft.Practices.Unity;

namespace Test.Core
{
    public class UserUnityRegistration : IUnityRegistration
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
        }
    }
}