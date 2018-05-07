using Test.Core;
using Unity;

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