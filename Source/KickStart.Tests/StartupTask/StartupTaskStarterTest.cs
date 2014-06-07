using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KickStart.StartupTask;
using Test.Core;
using Test.Core.Startup;
using Xunit;

namespace KickStart.Tests.StartupTask
{
    public class StartupTaskStarterTest
    {

        [Fact]
        public void Configure()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<TestStartup>()
                .UseStartupTask()
                .LogLevel(TraceLevel.Verbose)
            );
        }

        [Fact]
        public void ConfigureUseContainer()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<TestStartup>()
                .UseStartupTask(c => c.UseContainer())
                .LogLevel(TraceLevel.Verbose)
            );
        }
    }

}
