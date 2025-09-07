using KickStart.Services;

using Ninject;

using Test.Core;

namespace KickStart.Ninject.Tests;

public class NinjectStarterTest
{
    private readonly ITestOutputHelper _output;

    public NinjectStarterTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void UseNinject()
    {
        Kick.Start(config => config
            .LogTo(_output.WriteLine)
            .IncludeAssemblyFor<UserNinjectModule>()
            .UseNinject()
        );

        Kick.ServiceProvider.Should().NotBeNull();
        Kick.ServiceProvider.Should().BeOfType<StandardKernel>();
        Kick.ServiceProvider.As<IKernel>().Should().BeOfType<StandardKernel>();

        var repo = Kick.ServiceProvider.GetService<IUserRepository>();
        repo.Should().NotBeNull();
        repo.Should().BeOfType<UserRepository>();
    }

}
