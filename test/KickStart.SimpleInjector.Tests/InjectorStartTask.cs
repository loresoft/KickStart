using KickStart.StartupTask;

using Test.Core;

namespace KickStart.SimpleInjector.Tests;

public class InjectorStartTask : StartupTaskBase
{
    private readonly IUserService _userService;

    public InjectorStartTask(IUserService userService)
    {
        _userService = userService;
    }

    public override void Run(IDictionary<string, object> data)
    {
        _userService.Should().NotBeNull();
        _userService.Connection.Should().NotBeNull();
    }
}
