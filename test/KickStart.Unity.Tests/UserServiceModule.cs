using KickStart.Services;

using Test.Core;

namespace KickStart.Unity.Tests
{
    public class UserServiceModule : IServiceModule
    {
        public void Register(IServiceRegistration services, IDictionary<string, object> data)
        {
            services.RegisterSingleton<IConnection, SampleConnection>();
            services.RegisterTransient<IUserService, UserService>();
        }
    }
}