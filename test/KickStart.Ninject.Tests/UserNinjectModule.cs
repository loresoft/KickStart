using Ninject.Modules;

using Test.Core;

namespace KickStart.Ninject.Tests
{
    public class UserNinjectModule : NinjectModule
    {        
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}