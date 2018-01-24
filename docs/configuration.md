# Configuration

KickStart uses a fluent configuration model.  To start configuration, use the `Kick.Start` delegate.

## Example

This example will scan the assembly containing UserModule.  Then it will find all Autofac modules and register them with Autofac.  Then, all AutoMapper profiles will be registered with AutoMapper. Finally, it will find all classes that implement `IStartupTask` and run it. 

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserModule>()
    .UseAutofac()
    .UseAutoMapper()
    .UseStartupTask()
);
```

## Logging

Output KickStart logs using the `LogTo` fluent configuration

Write all logs to Console

```csharp
Kick.Start(config => config
    .LogTo(Console.WriteLine)
);
```

Write all logs to NLog

```csharp
public class Startup
{
    private static NLog.Logger _logger = NLog.LogManager.GetLogger("Startup");

    public Startup(ITestOutputHelper output)
    {
        Kick.Start(config => config
            .LogTo(_logger.Debug)
        );
    }
}
```

Write all logs to xUnit

```csharp
public class UnitTest
{
    private readonly ITestOutputHelper _output;

    public UnitTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Configure()
    {
        Kick.Start(config => config
            .LogTo(_output.WriteLine)
        );
    }
}
```

## Assembly Scanning

KickStart uses assembly scanning to look for a particular type when a startup task is run. The following examples are how to configure which assemblies to scan.  If no assemblies are configured, the default behaviour is to scan all loaded assemblies.

Include Assembly containing particular type.

```csharp
Kick.Start(config => config
    .IncludeAssemblyFor<UserModule>()
);
```

Include the assemblies that contain the specified name.

```csharp
Kick.Start(config => config
    .IncludeName("Project")
);
```

Exclude Assembly containing particular type.

```csharp
Kick.Start(config => config
    .ExcludeAssemblyFor<UserModule>()
);
```

Exclude the assemblies that contain the specified name.

```csharp
Kick.Start(config => config
    .ExcludeName("Microsoft")
);
```

## Data Dictionary

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
