using System.Collections.Generic;
using System.Threading.Tasks;
using KickStart.StartupTask;

namespace KickStart.Tests.StartupTask
{
    public class MediumBTask : IStartupTask
    {
        public int Priority => 100;

        public Task RunAsync(IDictionary<string, object> data)
        {
            return Task.Delay(1100);
        }
    }

   

}
