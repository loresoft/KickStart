using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KickStart.Logging;
using KickStart.Services;
using SimpleInjector;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.SimpleInjector.Tests
{
    public class SimpleInjectorStarterTest
    {
        public SimpleInjectorStarterTest(ITestOutputHelper output)
        {
            var writer = new DelegateLogWriter(d => output.WriteLine(d.ToString()));
            Logger.RegisterWriter(writer);
        }

        [Fact]
        public void UseSimpleInjector()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserSimpleInjectorRegistration>()
                .UseSimpleInjector()
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<Container>();
            Kick.ServiceProvider.As<Container>().Should().BeOfType<Container>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseSimpleInjectorInitialize()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserSimpleInjectorRegistration>()
                .UseSimpleInjector(c => c
                    .Container(b => b.Register<Employee>())
                )
            );

            Kick.ServiceProvider.Should().NotBeNull();
            Kick.ServiceProvider.Should().BeOfType<Container>();
            Kick.ServiceProvider.As<Container>().Should().BeOfType<Container>();

            var repo = Kick.ServiceProvider.GetService<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.ServiceProvider.GetService<Employee>();
            employee.Should().NotBeNull();
        }

    }
}
