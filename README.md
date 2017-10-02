# KickStart

Application start-up helper to initialize things like an IoC container, register mapping information or run a task.

[![Build status](https://ci.appveyor.com/api/projects/status/lk092y48a2b9f8ys)](https://ci.appveyor.com/project/LoreSoft/kickstart)

| Package | Version |
| :--- | :--- |
| [KickStart](https://www.nuget.org/packages/KickStart/) |  [![KickStart](https://img.shields.io/nuget/v/KickStart.svg)](https://www.nuget.org/packages/KickStart/) |
| [KickStart.Autofac](https://www.nuget.org/packages/KickStart.Autofac/) |  [![KickStart.Autofac](https://img.shields.io/nuget/v/KickStart.Autofac.svg)](https://www.nuget.org/packages/KickStart.Autofac/) |
| [KickStart.AutoMapper](https://www.nuget.org/packages/KickStart.AutoMapper/) |  [![KickStart.AutoMapper](https://img.shields.io/nuget/v/KickStart.AutoMapper.svg)](https://www.nuget.org/packages/KickStart.AutoMapper/) |
| [KickStart.DependencyInjection](https://www.nuget.org/packages/KickStart.DependencyInjection/) |  [![KickStart.DependencyInjection](https://img.shields.io/nuget/v/KickStart.DependencyInjection.svg)](https://www.nuget.org/packages/KickStart.DependencyInjection/) |
| [KickStart.MongoDB](https://www.nuget.org/packages/KickStart.MongoDB/) |  [![KickStart.MongoDB](https://img.shields.io/nuget/v/KickStart.MongoDB.svg)](https://www.nuget.org/packages/KickStart.MongoDB/) |
| [KickStart.Ninject](https://www.nuget.org/packages/KickStart.Ninject/) |  [![KickStart.Ninject](https://img.shields.io/nuget/v/KickStart.Ninject.svg)](https://www.nuget.org/packages/KickStart.Ninject/) |
| [KickStart.SimpleInjector](https://www.nuget.org/packages/KickStart.SimpleInjector/) |  [![KickStart.SimpleInjector](https://img.shields.io/nuget/v/KickStart.SimpleInjector.svg)](https://www.nuget.org/packages/KickStart.SimpleInjector/) |
| [KickStart.Unity](https://www.nuget.org/packages/KickStart.Unity/) |  [![KickStart.Unity](https://img.shields.io/nuget/v/KickStart.Unity.svg)](https://www.nuget.org/packages/KickStart.Unity/) |

## Download

The KickStart library is available on nuget.org via package name `KickStart`.

To install KickStart, run the following command in the Package Manager Console

    PM> Install-Package KickStart
    
More information about NuGet package available at
<https://nuget.org/packages/KickStart>

## Development Builds

Development builds are available on the myget.org feed.  A development build is promoted to the main NuGet feed when it's determined to be stable. 

In your Package Manager settings add the following package source for development builds:
<http://www.myget.org/F/loresoft/>

## Features

- Run tasks on application start-up
- Extension model to add library specific start up tasks
- Common IoC container adaptor based on `IServiceProvider`
- Singleton instance of an application level IoC container `Kick.ServiceProvider`


## Example

This example will scan the assembly containing UserModule.  Then it will find all Autofac modules and register them with Autofac.  Then, all AutoMapper profiles will be registered with Automapper. Finally, it will find all classes that implement `IStartupTask` and run it. 

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserModule>()
    .UseAutofac()
    .UseAutoMapper()
    .UseStartupTask()
);
```

Pass data to the startup modules

```csharp
Kick.Start(config => config
    .Data("environment", "debug")
    .Data(d =>
    {
        d["key"] = 123;
        d["server"] = "master";
    })
);
```

## Extensions

- StartupTask - Run any class that implements `IStartupTask`
- Autofac - Registers all Autofac `Module` classes and creates the container
- AutoMapper - Registers all AutoMapper `Profile` classes
- DependencyInjection - Register all `IDependencyInjectionRegistration` instances for Microsoft.Extensions.DependencyInjection
- MongoDB - Registers all `BsonClassMap` classes with MongoDB serialization
- Ninject - Registers all `NinjectModule` classes and creates an `IKernal`
- SimpleInjector - Run all `ISimpleInjectorRegistration` instances allowing container registration
- Unity - Run all `IUnityRegistration` instances allowing container registration

### StartupTask

The StartupTask extension allows running code on application start-up. To use this extension, implement the `IStartupTask` interface. Use the `Priority` property to control the order of execution.


Basic usage

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserModule>() // where to look for tasks
    .UseStartupTask() // include startup tasks in the Kick Start        
);
```

Run a delegate on startup

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserModule>()
    .UseAutofac() // init Autofac or any other IoC as container
    .UseStartupTask(c => c =>
    {
        c.Run((services, data) =>
        {
            //do work here
        });
    }) 
);
```

### Autofac

The Autofac extension allows registration of types to be resolved.  The extension also creates a default container and sets it to the `Kick.Container` singleton for access later.

Basic usage

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserRepository>() // where to look for tasks
    .UseAutofac() // initialize Autofac        
);
```

Use with ASP.NET MVC

```csharp
Kick.Start(c => c
    .IncludeAssemblyFor<UserModule>()
    .UseAutofac(a => a
        .Initialize(b => b.RegisterControllers(typeof(MvcApplication).Assembly)) // register all controllers 
        .Container(r => DependencyResolver.SetResolver(new AutofacDependencyResolver(r))) // set asp.net resolver
    )
    .UseAutoMapper()
    .UseMongoDB()
    .UseStartupTask()
);
```


To install Autofac extension, run the following command in the Package Manager Console

    PM> Install-Package KickStart.Autofac

### DependencyInjection

The DependencyInjection extension allows using Microsoft.Extensions.DependencyInjection for depenancy injection.

Basic Usage

```csharp
Kick.Start(config => config
    .LogTo(_output.WriteLine)
    .IncludeAssemblyFor<UserRepository>() // where to look
    .UseDependencyInjection() // initialize DependencyInjection
);
```

Integrate with asp.net core 2.0

```csharp
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // this will auto register logging and run the DependencyInjection startup
        services.KickStart(c => c
            .IncludeAssemblyFor<UserRepository>() // where to look
            .Data("configuration", Configuration) // pass configuration to all startup modules
            .Data("hostProcess", "web") // used for conditional registration
            .UseStartupTask() // run startup task
        );
    }
}
```

### SimpleInjector 

The SimpleInjector extension allows registration of types to be resolved by running all instances of `ISimpleInjectorRegistration`.  The extension also creates a default container and sets it to the `Kick.Container` singleton for access later.

Basic usage

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserRepository>() // where to look
    .UseSimpleInjector () // initialize SimpleInjector         
);
```

Using SimpleInjector with ASP.NET WebAPI

```csharp
Kick.Start(c => c
    .LogTo(_logger.Debug)
    .IncludeAssemblyFor<UserModule>()
    .Data("hostProcess", "web")
    .UseSimpleInjector(s => s
        .Verify(VerificationOption.VerifyOnly)
        .Initialize(container =>
        {
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle(); 
            container.RegisterWebApiControllers(httpConfiguration); // register all controllers 
        })
        .Container(container =>
        {
            httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container); // set asp.net resolver
        })
    )
    .UseStartupTask()
);
```

To install SimpleInjector extension, run the following command in the Package Manager Console

    PM> Install-Package KickStart.SimpleInjector

### Unity 

The Unity extension allows registration of types to be resolved by running all instances of `IUnityRegistration`.  The extension also creates a default container and sets it to the `Kick.Container` singleton for access later.

Basic usage

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserRepository>() // where to look
    .UseUnity () // initialize Unity         
);
```

To install Unity extension, run the following command in the Package Manager Console

    PM> Install-Package KickStart.Unity


### xUnit

Example of bootstraping and logging with xUnit tests.

```csharp
public class StartupTaskStarterTest
{
    private readonly ITestOutputHelper _output;

    public StartupTaskStarterTest(ITestOutputHelper output)
    {
        _output = output;

        // bootstrap project
        Kick.Start(config => config
            .LogTo(_output.WriteLine)
            .Data("enviroment", "test") // pass data for conditional registration
            .IncludeAssemblyFor<UserRepository>()
            .UseSimpleInjector () // initialize SimpleInjector
            .UseStartupTask()
        );

    }

    [Fact]
    public void RunTest()
    {
        var userRepository = Kick.ServiceProvider.GetService<IUserRepository>();
        Assert.NotNull(userRepository);

        // more tests
    }
}
```

## Generic Service Registration

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

## Change Log

### Version 6.0

- add assembly scanning for generic service registration
- [Breaking] internal change to `Context`.  Property Assemblies changed to Types.

### Version 5.0

- update to .net core 2

### Version 4.0

- [Breaking] Removed `IContainerAdaptor` and changed to use `IServiceProvider` instead
- [Breaking] Renamed `Kick.Container` to `Kick.ServiceProvider`
- [Breaking] Removed logging abstractions.  Logging is now a simple `Action<string>` delegate
- [Breaking] changed object creation to use ServiceProvider by default
- [Breaking] changed `IStartupTask.Run` to `IStartupTask.RunAsync` 
- changed startup tasks that run async and in parallel by Priority
- added shared data dictionary that is passed to all startup modules
- added delegate based startup action
- added `IServiceModule` and `IServiceRegistration` to abstract service/container registration
