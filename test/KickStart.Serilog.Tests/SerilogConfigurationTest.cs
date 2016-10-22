using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Test.Core;
using Xunit;
using Serilog;
using System.Reactive.Linq;
using System.Text;
using KickStart.Logging;
using Xunit.Abstractions;

namespace KickStart.Serilog.Tests
{
    public class SerilogConfigurationTest
    {
        private readonly ITestOutputHelper _output;

        public SerilogConfigurationTest(ITestOutputHelper output) 
        {
            _output = output;
        }

        [Fact]
        public void UseSerilog() 
        {
            var builder = new StringBuilder();

            Kick.Start(config => config
                .UseSerilog(c => c
                    .WriteTo.Observers(events => events
                        .Do(evt => {
                            builder.AppendLine($"LOG: {evt.RenderMessage()}");
                            _output.WriteLine($"LOG: {evt.RenderMessage()}");
                        })
                        .Subscribe()
                    )
                )
            );

            Logger.Info()
                .Message("This is a test message")
                .Write();

            builder.Length.Should().BeGreaterThan(0);
        }

    }
}
