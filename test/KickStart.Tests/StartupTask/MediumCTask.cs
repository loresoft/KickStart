using KickStart.StartupTask;

namespace KickStart.Tests.StartupTask;

public class MediumCTask : IStartupTask
{
    public int Priority => 100;

    public Task RunAsync(IDictionary<string, object> data)
    {
        return Task.Delay(100);
    }
}
