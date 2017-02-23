using System;
using KickStart.Services;

namespace Test.Core
{
    public class UserServiceModule : IServiceModule
    {
        public void Register(IServiceRegistration services)
        {
            services.RegisterSingleton<IConnection, SampleConnection>();
            services.RegisterTransient<IUserService, UserService>(c => new UserService(c.GetService<IConnection>()));
        }
    }
}