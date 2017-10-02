using System;
using System.Collections.Generic;
using KickStart.Services;

namespace Test.Core
{
    public class UserServiceModule : IServiceModule
    {
        public void Register(IServiceRegistration services, IDictionary<string, object> data)
        {
            services.RegisterSingleton<IConnection, SampleConnection>();
            services.RegisterTransient<IUserService, UserService>(c => new UserService(c.GetService<IConnection>()));


            //services.RegisterSingleton(r => r
            //    .Types(t => t.AssignableTo(typeof(IRepository<>)))
            //    .As(s => s.Self().ImplementedInterfaces())
            //);


            services.RegisterSingleton(r => r
                .Types(t => t.AssignableTo<IService>())
                .As(s => s.Self().ImplementedInterfaces())
            );

            services.RegisterSingleton(r => r
                .Types(t => t.AssignableTo<IVehicle>())
                .As(s => s.Self().ImplementedInterfaces())
            );

        }

    }
}