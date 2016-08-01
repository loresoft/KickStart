using System;
using KickStart.Unity;
using Microsoft.Practices.Unity;
using Test.Core;

namespace KickStart.Unity.Tests
{
    public class UserUnityRegistration : IUnityRegistration
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
        }
    }
}