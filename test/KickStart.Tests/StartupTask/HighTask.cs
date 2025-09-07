using KickStart.StartupTask;

namespace KickStart.Tests.StartupTask
{
    public class HighTask : IStartupTask
    {
        public int Priority => 10;

        public Task RunAsync(IDictionary<string, object> data)
        {
            return Task.Delay(1000);
        }
    }



}
