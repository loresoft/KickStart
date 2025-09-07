using Microsoft.Extensions.DependencyInjection;

using Test.Core;

namespace KickStart.DependencyInjection.Tests
{
    public class UserDependencyInjectionRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}