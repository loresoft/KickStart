using Test.Core.Startup;

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
        public void MultipleTasks()
        {
            //hack use logs to track execution order
            var logs = new List<string>();

            Kick.Start(config => config
                .LogTo(m =>
                {
                    logs.Add(m);
                    _output.WriteLine(m);
                })
                .IncludeAssemblyFor<TestStartup>()
                .IncludeAssemblyFor<HighTask>()
                .UseStartupTask()
            );


            int highExecute = logs.IndexOf("Execute Startup Task; Type: 'KickStart.Tests.StartupTask.HighTask'");
            int mediumExecuteA = logs.IndexOf("Execute Startup Task; Type: 'KickStart.Tests.StartupTask.MediumATask'");
            int mediumExecuteB = logs.IndexOf("Execute Startup Task; Type: 'KickStart.Tests.StartupTask.MediumBTask'");
            int mediumExecuteC = logs.IndexOf("Execute Startup Task; Type: 'KickStart.Tests.StartupTask.MediumCTask'");
            int mediumCompleteA = logs.FindIndex(m => m.StartsWith("Complete Startup Task; Type: 'KickStart.Tests.StartupTask.MediumATask'"));
            int mediumCompleteB = logs.FindIndex(m => m.StartsWith("Complete Startup Task; Type: 'KickStart.Tests.StartupTask.MediumBTask'"));
            int mediumCompleteC = logs.FindIndex(m => m.StartsWith("Complete Startup Task; Type: 'KickStart.Tests.StartupTask.MediumCTask'"));


            int startUpExecute = logs.IndexOf("Execute Startup Task; Type: 'Test.Core.Startup.TestStartup'");

            // check order by using log position
            highExecute.Should().BeLessThan(mediumExecuteA);
            highExecute.Should().BeLessThan(mediumExecuteB);
            highExecute.Should().BeLessThan(mediumExecuteC);

            mediumExecuteA.Should().BeLessThan(startUpExecute);
            mediumExecuteB.Should().BeLessThan(startUpExecute);
            mediumExecuteC.Should().BeLessThan(startUpExecute);

            // check parallel, b started before a completed
            mediumExecuteB.Should().BeLessThan(mediumCompleteA);
            // check parallel, c started before a completed
            mediumExecuteC.Should().BeLessThan(mediumCompleteA);
            // check parallel, c completed before a completed
            mediumCompleteC.Should().BeLessThan(mediumCompleteA);
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

