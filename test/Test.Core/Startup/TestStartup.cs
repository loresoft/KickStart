using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KickStart.StartupTask;

namespace Test.Core.Startup
{
    public class TestStartup : StartupTaskBase
    {
        public override void Run(IDictionary<string, object> data)
        {
            Console.WriteLine("Run Test Startup Test");
        }
    }
}
