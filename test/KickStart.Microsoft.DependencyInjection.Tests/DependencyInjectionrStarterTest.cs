using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KickStart.Logging;
using Microsoft.Extensions.DependencyInjection;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Microsoft.DependencyInjection.Tests
{
    public class DependencyInjectionStarterTest
    {
        public DependencyInjectionStarterTest(ITestOutputHelper output)
        {
            var writer = new DelegateLogWriter(d => output.WriteLine(d.ToString()));
            Logger.RegisterWriter(writer);
        }

        [Fact]
        public void UseSimpleInjector()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserDependencyInjectionRegistration>()
                .UseDependencyInjection()
            );

            Kick.ServiceProvider.Should().NotBeNull();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseSimpleInjectorInitialize()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserDependencyInjectionRegistration>()
                .UseDependencyInjection(c => c
                    .Services(b => b.AddTransient<Employee>())
                )
            );

            Kick.ServiceProvider.Should().NotBeNull();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.ServiceProvider.GetService<Employee>();
            employee.Should().NotBeNull();
        }

    }
}
