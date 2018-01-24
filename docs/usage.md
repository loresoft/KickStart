# Usage

The following are some example usages of KickStart.

## ASP.NET Core 2.0

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

## ASP.NET

Use Autofac with ASP.NET MVC

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

## xUnit

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
            .Data("environment", "test") // pass data for conditional registration
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