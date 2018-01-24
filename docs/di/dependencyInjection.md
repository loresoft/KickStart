# DependencyInjection

The DependencyInjection extension allows using Microsoft.Extensions.DependencyInjection for dependency injection.

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

To install DependencyInjection extension, run the following command in the Package Manager Console

    PM> Install-Package KickStart.DependencyInjection
