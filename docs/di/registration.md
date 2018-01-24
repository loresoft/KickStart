# Generic Service Registration

KickStart has a generic service registration abstraction.  This allows for the creation of a generic class module that registers services for dependency injection that is container agnostic.

Example module to register services

```csharp
public class UserServiceModule : IServiceModule
{
    public void Register(IServiceRegistration services, IDictionary<string, object> data)
    {
        services.RegisterSingleton<IConnection, SampleConnection>();
        services.RegisterTransient<IUserService, UserService>(c => new UserService(c.GetService<IConnection>()));

        // register all types that are assignable to IService
        services.RegisterSingleton(r => r
            .Types(t => t.AssignableTo<IService>())
            .As(s => s.Self().ImplementedInterfaces())
        );

        // register all types that are assignable to IVehicle
        services.RegisterSingleton(r => r
            .Types(t => t.AssignableTo<IVehicle>())
            .As(s => s.Self().ImplementedInterfaces())
        );
    }
}
```