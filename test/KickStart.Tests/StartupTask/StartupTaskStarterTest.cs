using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KickStart.Logging;
using KickStart.StartupTask;
using Test.Core;
using Test.Core.Startup;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Tests.StartupTask
{
    public class StartupTaskStarterTest
    {
        public StartupTaskStarterTest(ITestOutputHelper output)
        {
            var writer = new DelegateLogWriter(d => output.WriteLine(d.ToString()));
            Logger.RegisterWriter(writer);
        }

        [Fact]
        public void Configure()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<TestStartup>()
                .UseStartupTask()
            );
        }

        [Fact]
        public void ConfigureUseContainer()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<TestStartup>()
                .UseStartupTask(c => c.UseContainer())
            );
        }
    }

}
