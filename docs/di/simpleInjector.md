# SimpleInjector

The SimpleInjector extension allows registration of types to be resolved by running all instances of `ISimpleInjectorRegistration`.

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