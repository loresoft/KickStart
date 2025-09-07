using KickStart.StartupTask;

namespace Test.Core.Startup;

public class TestStartup : StartupTaskBase
{
    public override void Run(IDictionary<string, object> data)
    {
        Console.WriteLine("Run Test Startup Test");
    }
}
