# Autofac

The Autofac extension allows registration of types to be resolved.  

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