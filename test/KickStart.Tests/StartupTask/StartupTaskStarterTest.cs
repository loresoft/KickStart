using FluentAssertions;
using Test.Core.Startup;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Tests.StartupTask
{
    public class StartupTaskStarterTest
    {
        private readonly ITestOutputHelper _output;

        public StartupTaskStarterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Configure()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<TestStartup>()
                .UseStartupTask()
            );
        }

        [Fact]
        public void ConfigureData()
        {


            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .Data("name", "value")
                .Data(d =>
                {
                    d["key"] = 123;
                    d["enviroment"] = "debug";
                })
                .IncludeAssemblyFor<TestStartup>()
                .UseStartupTask(c =>
                {
                    c.Run((services, data) =>
                    {
                        data.Should().ContainKey("name");
                        data.Should().ContainKey("key");
                        data.Should().ContainKey("enviroment");
                        data["enviroment"].Should().Be("debug");

                        data["passed"] = "yes";
                    });

                    c.Run((services, data) =>
                    {
                        data.Should().ContainKey("name");
                        data.Should().ContainKey("key");
                        data.Should().ContainKey("enviroment");
                        data["enviroment"].Should().Be("debug");
                        data["passed"].Should().Be("yes");

                    });
                })
            );
        }
    }

}
