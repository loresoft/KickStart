using System;
using Ninject.Modules;

namespace Test.Core
{
    public class UserNinjectModule : NinjectModule
    {        
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}