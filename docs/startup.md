# Startup Task

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